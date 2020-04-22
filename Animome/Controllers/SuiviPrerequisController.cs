using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;

namespace Animome.Controllers
{
    public class SuiviPrerequisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviPrerequisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviPrerequis
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = from s in _context.SuiviPrerequis select s;

            suiviPrerequis = _context.SuiviPrerequis.Where(x => x.SuiviCompetence.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices);

            return View(await suiviPrerequis.ToListAsync());
        }

        public async Task<IActionResult> AfficherPrerequis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviPrerequis = _context.SuiviPrerequis.Where(x => x.Id == id)
                 .Include(suiviPrerequis => suiviPrerequis.Prerequis)
                 .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                     .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                 .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                     .ThenInclude(lesSuiviNivx => lesSuiviNivx.Niveau)
                  .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                     .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesNotes)
                  .Include(x => x.SuiviCompetence.Suivi.Patient);

            return View(await suiviPrerequis.SingleOrDefaultAsync());
        }

        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviPrerequis = await _context.SuiviPrerequis.Where(x => x.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence)
                    .ThenInclude(sc => sc.Suivi)
                .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence)
                .Include(x=>x.SuiviCompetence.Suivi.Patient)
                .SingleAsync();


            try
            {
                if (suiviPrerequis.Etat != EtatEnum.e3)
                {
                    suiviPrerequis.Etat = EtatEnum.e3;
                    suiviPrerequis.DateValide = DateTime.Now;

                    foreach (SuiviNiveau sn in suiviPrerequis.LesSuiviNiveaux)
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
                        }
                        _context.Update(sn);
                    }
                    _context.Update(suiviPrerequis);
                    await _context.SaveChangesAsync();

                    MajEtats(suiviPrerequis);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviPrerequisExists(suiviPrerequis.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new { suiviPrerequis.Id});
        }

        public async Task<IActionResult> AnnulerValidation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis.Where(x => x.Id == id)
                   .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                       .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                   .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence)
                       .ThenInclude(sc => sc.Suivi)
                   .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence)
                   .Include(x => x.SuiviCompetence.Suivi.Patient)
                   .SingleAsync();

            try
            {
                if (suiviPrerequis.Etat != EtatEnum.e1)
                {
                    suiviPrerequis.Etat = EtatEnum.e1;
                    suiviPrerequis.DateValide = DateTime.MinValue;
                   
                    foreach (SuiviNiveau sn in suiviPrerequis.LesSuiviNiveaux)
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
                        }
                        _context.Update(sn);
                    }
                    _context.Update(suiviPrerequis);
                    await _context.SaveChangesAsync();

                    MajEtats(suiviPrerequis);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviPrerequisExists(suiviPrerequis.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherSuivi", "Suivis", new { suiviPrerequis.SuiviCompetence.Suivi.Patient.Id });
        }

        private bool SuiviPrerequisExists(int id)
        {
            return _context.SuiviPrerequis.Any(e => e.Id == id);
        }

        /// <summary>
        /// Suite au changement d'état d'un suiviPrerequis, mise à jour en conséquence des éléments de niveaux supérieur (SuiviCompetence, Suivi)
        /// </summary>
        /// <param name="suiviPrerequis"></param>
        private void MajEtats(SuiviPrerequis suiviPrerequis)
        {
            var suiviCompetence = _context.SuiviCompetence.Where(x => x.Id == suiviPrerequis.SuiviCompetence.Id)
                .Include(x=>x.LesSuiviPrerequis)
                .Single();
            suiviCompetence.Etat = suiviCompetence.EtatMaj();
            _context.Update(suiviCompetence);

            var suivi =  _context.Suivi.Where(x=>x.Id==suiviCompetence.Suivi.Id)
                .Include(x=>x.LesSuiviCompetences)
                .Single();
            suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
        }
       
    }
}
