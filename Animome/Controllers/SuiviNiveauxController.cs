﻿using System;
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
    public class SuiviNiveauxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviNiveauxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviNiveaux
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuiviNiveau.ToListAsync());
        }

       
        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviNiveau = await _context.SuiviNiveau.Where(x => x.Id == id)
                .Include(suiviNiveau => suiviNiveau.Niveau)
                .Include(suiviNiveau => suiviNiveau.LesSuiviExercices)
                 .Include(s => s.SuiviPrerequis)
                                .ThenInclude(sp => sp.SuiviCompetence)
                                    .ThenInclude(sc => sc.Suivi)

                .SingleAsync();

            try
            {
                if (suiviNiveau.Etat != EtatEnum.e3)
                {
                    suiviNiveau.Etat = EtatEnum.e3;
                    suiviNiveau.DateValide = DateTime.Now;

                    foreach (SuiviExercice se in suiviNiveau.LesSuiviExercices)
                    {
                        if (!se.Valide)
                        {
                            se.Valide = true;
                            se.DateValide = DateTime.Now;
                        }
                    }
                    _context.Update(suiviNiveau);
                    await _context.SaveChangesAsync();
                    MajEtats(suiviNiveau);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviNiveauExists(suiviNiveau.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new { suiviNiveau.SuiviPrerequis.Id});
        }

        public async Task<IActionResult> AnnulerValidation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau.Where(x => x.Id == id)
                            .Include(suiviNiveau => suiviNiveau.Niveau)
                            .Include(suiviNiveau => suiviNiveau.LesSuiviExercices)
                            .Include(s => s.SuiviPrerequis)
                                .ThenInclude(sp=>sp.SuiviCompetence)
                                    .ThenInclude(sc=>sc.Suivi)
                            .SingleOrDefaultAsync();

                          
            try
            {
                if (suiviNiveau.Etat != EtatEnum.e1)
                {
                    suiviNiveau.Etat = EtatEnum.e1;
                    suiviNiveau.DateValide = DateTime.MinValue;

                    foreach (SuiviExercice se in suiviNiveau.LesSuiviExercices)
                    {
                        if (se.Valide)
                        {
                            se.Valide = false;
                            se.DateValide = DateTime.MinValue;
                            _context.Update(se);
                        }
                    }
                    _context.Update(suiviNiveau);
                    MajEtats(suiviNiveau);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviNiveauExists(suiviNiveau.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new { suiviNiveau.SuiviPrerequis.Id });
        }


        private bool SuiviNiveauExists(int id)
        {
            return _context.SuiviNiveau.Any(e => e.Id == id);
        }

        /// <summary>
        /// Maj des Etats des éléments de suivi du dessus
        /// </summary>
        /// <param name="suiviNiveau"></param>
        private void MajEtats(SuiviNiveau suiviNiveau)
        {
            var suiviPrerequis =  _context.SuiviPrerequis.Where(x => x.Id == suiviNiveau.SuiviPrerequis.SuiviCompetence.Id)
                       .Include(s => s.LesSuiviNiveaux)
                       .Single();

            suiviPrerequis.Etat = suiviPrerequis.EtatMaj();
            _context.Update(suiviPrerequis);

           var  suiviCompetence =  _context.SuiviCompetence.Where(x => x.Id == suiviPrerequis.SuiviCompetence.Id)
                .Include(s => s.LesSuiviPrerequis)
                .Single();
            suiviCompetence.Etat = suiviCompetence.EtatMaj();
            _context.Update(suiviCompetence);

           var  suivi =  _context.Suivi.Where(x => x.Id == suiviCompetence.Suivi.Id)
                .Include(s => s.LesSuiviCompetences)
                .Single();
            suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
        }
    }
}
