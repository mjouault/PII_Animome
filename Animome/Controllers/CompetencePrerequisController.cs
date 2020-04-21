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

namespace Animome.Controllers
{
    [Authorize (Roles ="Admin")]
    public class CompetencePrerequisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetencePrerequisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompetencePrerequis
        public async Task<IActionResult> Index()
        {
            var competencePrerequis = await _context.CompetencePrerequis
             .Include(cp => cp.Prerequis)
             .Include(dc => dc.Competence)
             .ToListAsync();

            return View(competencePrerequis);
        }


        /// <summary>
        /// Création de nouvelles associations compétences -Prérequis
        /// </summary>
        /// <returns></returns>
        public async Task <IActionResult> Create()
        {
            IQueryable<string> CompetenceQuery = from x in _context.Competence
                                                 orderby x.Intitule
                                                 select x.Intitule;
            IQueryable<string> PrerequisQuery = from x in _context.Prerequis
                                                 orderby x.Intitule
                                                 select x.Intitule;

            var viewModel = new CompetencePrerequisCreateViewModel
            {
                ListeCompetences = new SelectList(await CompetenceQuery.Distinct().ToListAsync()),
                ListePrerequis = new SelectList(await PrerequisQuery.Distinct().ToListAsync()),
            };
            return View(viewModel);
        }

        // POST: CompetencePrerequis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetencePrerequisCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var competence = await _context.Competence
             .FirstOrDefaultAsync(m => m.Intitule == viewModel.Competence.Intitule);

                var prerequis = await _context.Prerequis
             .FirstOrDefaultAsync(m => m.Intitule == viewModel.Prerequis.Intitule);

                if (competence != null && prerequis != null)
                {
                    var CompetencePrerequisAjoute = new CompetencePrerequis
                    {
                        Competence = competence,
                        Prerequis = prerequis
                    };

                    _context.Add(CompetencePrerequisAjoute);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Id", "Erreur : veuillez choisir un domaine/une compétence");
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }

        // GET: CompetencePrerequis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencePrerequis = await _context.CompetencePrerequis.FindAsync(id);
            if (competencePrerequis == null)
            {
                return NotFound();
            }
            return View(competencePrerequis);
        }

        // POST: CompetencePrerequis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] CompetencePrerequis competencePrerequis)
        {
            if (id != competencePrerequis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competencePrerequis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetencePrerequisExists(competencePrerequis.Id))
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
            return View(competencePrerequis);
        }

        /// <summary>
        /// Suppression d'un 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competencePrerequis = await _context.CompetencePrerequis
                .Include(x=>x.Competence)
                .Include(x=>x.Prerequis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competencePrerequis == null)
            {
                return NotFound();
            }

            return View(competencePrerequis);
        }

        // POST: CompetencePrerequis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competencePrerequis = await _context.CompetencePrerequis.FindAsync(id);
            _context.CompetencePrerequis.Remove(competencePrerequis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetencePrerequisExists(int id)
        {
            return _context.CompetencePrerequis.Any(e => e.Id == id);
        }

        private bool AlreadyExists(Prerequis prerequis, Competence competence)
        {
            return _context.CompetencePrerequis.Any(e => e.Prerequis.Intitule == prerequis.Intitule && e.Competence.Intitule==competence.Intitule) ;
        }
    }
}
