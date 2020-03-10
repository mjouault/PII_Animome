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

        // GET: SuiviCompetences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviCompetence == null)
            {
                return NotFound();
            }

            return View(suiviCompetence);
        }

        // GET: SuiviCompetences/Create
        /* public async Task <IActionResult> Create(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var suivi = await _context.Suivi.FindAsync(id);
             if (suivi == null)
             {
                 return NotFound();
             }
             return View();
         }

         // POST: SuiviCompetences/Create
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(int id, [Bind("Id,Suivi,Competence")] SuiviCompetence suiviCompetence)
         {
               var suivi = await _context.Suivi
                .FirstOrDefaultAsync(m => m.Id == id);

                 SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                 {
                     Competence = suiviCompetence.Competence,
                     Suivi = suivi
                 };
                 _context.Add(suiviCompetenceAjoute);
                 await _context.SaveChangesAsync();
             return RedirectToAction("Index", "Patients");
             //return RedirectToAction("AfficherSuivi", "Suivis", new { suivi.Patient.Id });
         }*/

        //GET AjouterCompetence
         public async Task<IActionResult> AjouterCompetence(int? id, SuiviEditViewModel viewModel)
         {
             if (id == null)
             {
                 return NotFound();
             }

             viewModel.Suivi = await _context.Suivi.FindAsync(id);
             if (viewModel.Suivi == null)
             {
                 return NotFound();
             }
             return View(viewModel);
         }

         // POST AjouterCompetence
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [Authorize]
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> AjouterCompetence(int id, SuiviEditViewModel viewModel)
         {
             if (id != viewModel.Suivi.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 var suivi = await _context.Suivi
                 .FirstOrDefaultAsync(m => m.Id == id);

                 SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                 {
                     Competence = viewModel.SuiviCompetence.Competence,
                     Suivi = suivi
                 };

                SuiviPrerequis suiviPrerequisAjoute = new SuiviPrerequis
                {
                    Prerequis = viewModel.SuiviPrerequis.Prerequis,
                    SuiviCompetence = suiviCompetenceAjoute,
                };

                SuiviNiveau suiviNiveauAjoute = new SuiviNiveau
                {
                    Niveau = viewModel.SuiviNiveau.Niveau,
                    SuiviPrerequis = suiviPrerequisAjoute,
                };

                SuiviExercice suiviExerciceAjoute = new SuiviExercice
                {
                   // Exercice = viewModel.SuiviExercice.Exercice,
                    Valide = false,
                    SuiviNiveau = suiviNiveauAjoute,
                };

                _context.Add(suiviCompetenceAjoute);
                _context.Add(suiviPrerequisAjoute);
                _context.Add(suiviNiveauAjoute);
                _context.Add(suiviExerciceAjoute);

                 await _context.SaveChangesAsync();
                 return RedirectToAction( "Index", "Patients");
             }
             return View(viewModel);
         }


        // GET: SuiviCompetences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence.FindAsync(id);
            if (suiviCompetence == null)
            {
                return NotFound();
            }
            return View(suiviCompetence);
        }

        // POST: SuiviCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Competence")] SuiviCompetence suiviCompetence)
        {
            if (id != suiviCompetence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviCompetence);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(suiviCompetence);
        }

        // GET: SuiviCompetences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviCompetence == null)
            {
                return NotFound();
            }

            return View(suiviCompetence);
        }

        // POST: SuiviCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviCompetence = await _context.SuiviCompetence.FindAsync(id);

            var PrerequisASupprimer = from x in _context.SuiviPrerequis
                                      select x;
            var NiveauxASupprimer = from x in _context.SuiviNiveau
                                    select x;
            var ExercicesASupprimer = from x in _context.SuiviExercice
                                    select x;

            PrerequisASupprimer = PrerequisASupprimer.Where(p => (p.SuiviCompetence.Id == id));
            NiveauxASupprimer = NiveauxASupprimer.Where(n => (n.SuiviPrerequis.SuiviCompetence.Id == id));
            ExercicesASupprimer = ExercicesASupprimer.Where(e => (e.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Id == id));
           
            _context.SuiviExercice.RemoveRange(await ExercicesASupprimer.ToListAsync());
            _context.SuiviNiveau.RemoveRange(await NiveauxASupprimer.ToListAsync());
            _context.SuiviPrerequis.RemoveRange(await PrerequisASupprimer.ToListAsync());
            _context.SuiviCompetence.Remove(suiviCompetence);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patients");
            //return RedirectToAction("AfficherSuivi", "Suivis", new { suiviCompetence.Suivi.Patient.Id });
        }

        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence1 = _context.SuiviCompetence.Where(x => x.Id == id)
                .Include(suiviCompetence => suiviCompetence.LesSuiviPrerequis)
                    .ThenInclude(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices);
            var suiviCompetence = await suiviCompetence1.SingleAsync();

            try
            {
                if (!suiviCompetence.Valide)
                {
                    suiviCompetence.Valide = true;
                    suiviCompetence.DateValide = DateTime.Now;

                    foreach (SuiviPrerequis sp in suiviCompetence.LesSuiviPrerequis)
                    {
                        sp.Valide = true;
                        sp.DateValide = DateTime.Now;

                        foreach (SuiviNiveau sn in sp.LesSuiviNiveaux)
                        {
                            sn.Valide = true;
                            sn.DateValide = DateTime.Now;
                            _context.Update(sn);

                            foreach (SuiviExercice se in sn.LesSuiviExercices)
                            {
                                sn.Valide = true;
                                sn.DateValide = DateTime.Now;
                                _context.Update(se);
                            }
                        }
                        _context.Update(sp);
                    }
                     _context.Update(suiviCompetence);
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
            return RedirectToAction("Index", "Patients");
        }

        public async Task<IActionResult> AnnulerValidation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence1 = _context.SuiviCompetence.Where(x => x.Id == id)
            .Include(suiviCompetence => suiviCompetence.LesSuiviPrerequis)
                .ThenInclude(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices);
                    var suiviCompetence = await suiviCompetence1.SingleAsync();

            try
            {
                if (suiviCompetence.Valide)
                {
                    suiviCompetence.Valide = false;
                    suiviCompetence.DateValide = DateTime.MinValue;

                    foreach (SuiviPrerequis sp in suiviCompetence.LesSuiviPrerequis)
                    {
                        sp.Valide = false;
                        sp.DateValide = DateTime.MinValue;

                        foreach (SuiviNiveau sn in sp.LesSuiviNiveaux)
                        {
                            sn.Valide = false;
                            sn.DateValide = DateTime.MinValue;

                            foreach (SuiviExercice se in sn.LesSuiviExercices)
                            {
                                sn.Valide = false;
                                sn.DateValide = DateTime.MinValue;
                            }
                        }
                    }
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
            return RedirectToAction("Index", "Patients");
        }

        private bool SuiviCompetenceExists(int id)
        {
            return _context.SuiviCompetence.Any(e => e.Id == id);
        }
    }
}
