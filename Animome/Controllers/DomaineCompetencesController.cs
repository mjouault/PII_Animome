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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Animome.Controllers
{
    [Authorize]
    public class DomaineCompetencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DomaineCompetencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DomaineCompetences
        public async Task<IActionResult> Index()
        {
            var domaineComp = await _context.DomaineCompetence
             .Include(dc => dc.Domaine)
             .Include(dc => dc.Competence)
             .ToListAsync();

            return View(domaineComp);
        }

        // GET: DomaineCompetences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaineCompetence = await _context.DomaineCompetence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaineCompetence == null)
            {
                return NotFound();
            }

            return View(domaineCompetence);
        }

        // GET: DomaineCompetences/Create
        public async Task<IActionResult> Create()
        {
            IQueryable<string> DomaineQuery = from x in _context.Domaine
                                              orderby x.Intitule
                                              select x.Intitule;

            IQueryable<string> CompetenceQuery = from x in _context.Competence
                                                 orderby x.Intitule
                                                 select x.Intitule;

            var viewModel = new DomaineCompetencesCreateViewModel
            {
                Domaines = new SelectList(await DomaineQuery.Distinct().ToListAsync()),
                Competences = new SelectList(await CompetenceQuery.Distinct().ToListAsync())
            };
            return View(viewModel);
        }

        // POST: DomaineCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DomaineCompetencesCreateViewModel viewModel)
        {
            if (AlreadyExists(viewModel.Domaine, viewModel.Competence))
            {
                ModelState.AddModelError("Intitule", "Erreur : élément déjà existant");
            }

            if (ModelState.IsValid)
            {
                var domaine = await _context.Domaine
               .FirstOrDefaultAsync(m => m.Intitule == viewModel.Domaine.Intitule);

                var competence = await _context.Competence
              .FirstOrDefaultAsync(m => m.Intitule == viewModel.Competence.Intitule);

                var domaineCompetenceAjoute = new DomaineCompetence
                {
                    Domaine = domaine,
                    Competence = competence
                };

                _context.Add(domaineCompetenceAjoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }



        // GET: DomaineCompetences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaineCompetence = await _context.DomaineCompetence.FindAsync(id);
            if (domaineCompetence == null)
            {
                return NotFound();
            }
            return View(domaineCompetence);
        }

        // POST: DomaineCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] DomaineCompetence domaineCompetence)
        {
            if (id != domaineCompetence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domaineCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomaineCompetenceExists(domaineCompetence.Id))
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
            return View(domaineCompetence);
        }

        // GET: DomaineCompetences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaineCompetence = await _context.DomaineCompetence
                .Include(x=>x.Domaine)
                .Include(x=>x.Competence)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaineCompetence == null)
            {
                return NotFound();
            }

            return View(domaineCompetence);
        }

        // POST: DomaineCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domaineCompetence = await _context.DomaineCompetence.FindAsync(id);
            _context.DomaineCompetence.Remove(domaineCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomaineCompetenceExists(int id)
        {
            return _context.DomaineCompetence.Any(e => e.Id == id);
        }

        private bool AlreadyExists(Domaine d, Competence c)
        {
            return _context.DomaineCompetence.Any(e => e.Domaine.Intitule == d.Intitule && e.Competence.Intitule == c.Intitule);
        }
    }
}
