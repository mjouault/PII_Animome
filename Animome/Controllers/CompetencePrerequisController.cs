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
             .Include(cp => cp.Competence)
             .OrderBy(cp => cp.Competence.Intitule)
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
            ViewData["erreur"] = "";
            return View(viewModel);
        }

        // POST: CompetencePrerequis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetencePrerequisCreateViewModel viewModel)
        {
            ViewData["erreur"] = "";

            if (AlreadyExists(viewModel.Competence, viewModel.Prerequis))
            {
                ViewData["erreur"] = "Element déjà existant";
                ModelState.AddModelError("Intitule", "element existant");
            }

            if (ModelState.IsValid)
            {
                var competence = await _context.Competence
             .FirstOrDefaultAsync(m => m.Intitule == viewModel.Competence.Intitule);

                var prerequis = await _context.Prerequis
             .FirstOrDefaultAsync(m => m.Intitule == viewModel.Prerequis.Intitule);

                    var CompetencePrerequisAjoute = new CompetencePrerequis
                    {
                        Competence = competence,
                        Prerequis = prerequis
                    };

                    _context.Add(CompetencePrerequisAjoute);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }

            //Permet d'afficher de nouveau les noms des compétences et prérequis dans les SelectList en cas d'erreur
            else
            {
                IQueryable<string> CompetenceQuery = from x in _context.Competence
                                                     orderby x.Intitule
                                                     select x.Intitule;
                IQueryable<string> PrerequisQuery = from x in _context.Prerequis
                                                    orderby x.Intitule
                                                    select x.Intitule;


                viewModel.ListeCompetences = new SelectList(await CompetenceQuery.Distinct().ToListAsync());
                viewModel.ListePrerequis = new SelectList(await PrerequisQuery.Distinct().ToListAsync());
            }
            return View(viewModel);
        }


        /// <summary>
        /// Suppression d'une instance
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

        private bool AlreadyExists(Competence c, Prerequis p)
        {
            return _context.CompetencePrerequis.Any(e => e.Competence.Intitule == c.Intitule && e.Prerequis.Intitule == p.Intitule);
        }
    }
}
