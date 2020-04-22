using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;
using Animome.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Animome.Controllers
{
    public class SuiviCompetencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviCompetencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviCompetences
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuiviCompetence.ToListAsync());
        }

        /// <summary>
        /// Permet à un utilisateur de valider/Marquer une compétence comme acquise pour un patient donné, ainsi que tous les éléments de niveaux inférieur qui la compose
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence.Where(x => x.Id == id)
                .Include(suiviCompetence => suiviCompetence.LesSuiviPrerequis)
                    .ThenInclude(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(SuiviCompetence => SuiviCompetence.Suivi)
                .Include(suiviCompetence=>suiviCompetence.Suivi.Patient)
                .SingleAsync();

            try
            {
                //Changement de l'Etat de non acquis (e1) ou en cours (e2) à validé (e3) du suiviCompetence et de tous les éléments qui le composent (suiviPrerequis, suiviNiveau, suiviExercice)
                if (suiviCompetence.Etat!=EtatEnum.e3)
                {
                    suiviCompetence.Etat = EtatEnum.e3;
                    suiviCompetence.DateValide = DateTime.Now;

                    foreach (SuiviPrerequis sp in suiviCompetence.LesSuiviPrerequis)
                    {
                        if (sp.Etat != EtatEnum.e3)
                        {
                            sp.Etat = EtatEnum.e3;
                            sp.DateValide = DateTime.Now;

                            foreach (SuiviNiveau sn in sp.LesSuiviNiveaux)
                            {
                                if (sn.Etat != EtatEnum.e3)
                                {
                                    sn.Etat = EtatEnum.e3;
                                    sn.DateValide = DateTime.Now;

                                    foreach (SuiviExercice se in sn.LesSuiviExercices)
                                    {
                                        if (!se.Valide)
                                        {
                                            se.Valide = true;
                                            se.DateValide = DateTime.Now;
                                            _context.Update(se);
                                        }
                                    }
                                    _context.Update(sn);
                                }
                            }
                        }
                        _context.Update(sp);
                    }
                    _context.Update(suiviCompetence);

                    MajEtats(suiviCompetence);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviCompetenceExists(suiviCompetence.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherSuivi", "Suivis", new { suiviCompetence.Suivi.Patient.Id});
        }

        /// <summary>
        /// Permet à un utilisateur d'annuler la validation d'une compétence pour un patient donné
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AnnulerValidation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence.Where(x => x.Id == id)
            .Include(suiviCompetence => suiviCompetence.LesSuiviPrerequis)
                .ThenInclude(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
            .Include(suiviCompetence => suiviCompetence.Suivi)
            .Include(suiviCompetence => suiviCompetence.Suivi.Patient)
            .SingleAsync();


            try
            {
                if (suiviCompetence.Etat != EtatEnum.e1)
                {
                    suiviCompetence.Etat = EtatEnum.e1 ;
                    suiviCompetence.DateValide = DateTime.MinValue;

                    foreach (SuiviPrerequis sp in suiviCompetence.LesSuiviPrerequis)
                    {
                        if (sp.Etat != EtatEnum.e1)
                        {
                            sp.Etat = EtatEnum.e1;
                            sp.DateValide = DateTime.MinValue;

                            foreach (SuiviNiveau sn in sp.LesSuiviNiveaux)
                            {
                                if (sn.Etat != EtatEnum.e1)
                                {
                                    sn.Etat = EtatEnum.e1;
                                    sn.DateValide = DateTime.MinValue;

                                    foreach (SuiviExercice se in sn.LesSuiviExercices)
                                    {
                                        if (se.Valide)
                                        {
                                            se.Valide = false;
                                            se.DateValide = DateTime.MinValue;
                                            _context.Update(se);
                                        }
                                    }
                                    _context.Update(sn);
                                }
                            }
                            _context.Update(sp);
                        }  
                    }
                    _context.Update(suiviCompetence);

                    MajEtats(suiviCompetence);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviCompetenceExists(suiviCompetence.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherSuivi", "Suivis", new { suiviCompetence.Suivi.Patient.Id});
        }

        private bool SuiviCompetenceExists(int id)
        {
            return _context.SuiviCompetence.Any(e => e.Id == id);
        }

        /// <summary>
        /// Suite au changement d'état d'un suiviCompetence, mise à jour en conséquence des éléments de niveaux supérieur (Suivi)
        /// </summary>
        /// <param name="suiviCompetence"></param>
        public void MajEtats(SuiviCompetence suiviCompetence)
        {
            var suivi = _context.Suivi.Where(x => x.Id == suiviCompetence.Suivi.Id)
                 .Include(x=>x.LesSuiviCompetences)
                 .Single();
           suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
        }
    }
}
