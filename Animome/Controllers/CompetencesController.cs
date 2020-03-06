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

namespace Animome.Controllers
{
    public class CompetencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Competences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Competence.ToListAsync());
        }

        // GET: Competences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competence = await _context.Competence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // GET: Competences/Create
        public async Task<IActionResult> Create()
        {
            IQueryable<string> DomaineQuery = from x in _context.Domaine
                                              orderby x.Intitule
                                              select x.Intitule;
            var viewModel = new CompetenceCreateViewModel
            {
                Domaines = new SelectList(await DomaineQuery.Distinct().ToListAsync()),
            };
            return View(viewModel);
        }

        // POST: Competences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetenceCreateViewModel viewModel)
        {

            if (string.IsNullOrEmpty(viewModel.IntituleDomaine))
            {
                return NotFound();
            }
            else
            {
                var domaine = await _context.Domaine
                .FirstOrDefaultAsync(m => m.Intitule == viewModel.IntituleDomaine);

                var domaineCompetenceAjoute = new DomaineCompetence
                {
                    Domaine = domaine,
                    Competence = viewModel.Competence
                };

                var competenceAjoutee = new Competence
                {
                    Intitule = viewModel.Competence.Intitule
                };

                _context.Add(domaineCompetenceAjoute);
                _context.Add(competenceAjoutee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Competences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competence = await _context.Competence.FindAsync(id);
            if (competence == null)
            {
                return NotFound();
            }
            return View(competence);
        }

        // POST: Competences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intitule")] Competence competence)
        {
            if (id != competence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetenceExists(competence.Id))
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
            return View(competence);
        }

        // GET: Competences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competence = await _context.Competence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        // POST: Competences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competence = await _context.Competence.FindAsync(id);
            _context.Competence.Remove(competence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetenceExists(int id)
        {
            return _context.Competence.Any(e => e.Id == id);
        }
    }
}
