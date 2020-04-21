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
    [Authorize]
    public class PrerequisNiveauxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrerequisNiveauxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrerequisNiveaux
        public async Task<IActionResult> Index()
        {
            var prerequisNiveau = await _context.PrerequisNiveau
             .Include(pn => pn.Prerequis)
             .Include(pn => pn.Niveau)
             .ToListAsync();

            return View(prerequisNiveau);
        }

        // GET: PrerequisNiveaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prerequisNiveau = await _context.PrerequisNiveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prerequisNiveau == null)
            {
                return NotFound();
            }

            return View(prerequisNiveau);
        }

        // GET: PrerequisNiveaux/Create
        public async Task <IActionResult> Create()
        {
          
            IQueryable<string> PrerequisQuery = from x in _context.Prerequis
                                                orderby x.Intitule
                                                select x.Intitule;

            IQueryable<string> NiveauQuery = from x in _context.Niveau
                                                 orderby x.Intitule
                                                 select x.Intitule;

            var viewModel = new PrerequisNiveauxCreateViewModel
            {
                ListePrerequis = new SelectList(await PrerequisQuery.Distinct().ToListAsync()),
                ListeNiveaux = new SelectList(await NiveauQuery.Distinct().ToListAsync()),
            };
            return View(viewModel);
        }

        // POST: PrerequisNiveaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrerequisNiveauxCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var niveau = await _context.Niveau
             .FirstOrDefaultAsync(m => m.Intitule == viewModel.Niveau.Intitule);

                var prerequis = await _context.Prerequis
             .FirstOrDefaultAsync(m => m.Intitule == viewModel.Prerequis.Intitule);

                var PrerequisNiveauAjoute = new PrerequisNiveau
                {
                    Niveau = niveau,
                    Prerequis = prerequis
                };

                _context.Add(PrerequisNiveauAjoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: PrerequisNiveaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prerequisNiveau = await _context.PrerequisNiveau.FindAsync(id);
            if (prerequisNiveau == null)
            {
                return NotFound();
            }
            return View(prerequisNiveau);
        }

        // POST: PrerequisNiveaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] PrerequisNiveau prerequisNiveau)
        {
            if (id != prerequisNiveau.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prerequisNiveau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrerequisNiveauExists(prerequisNiveau.Id))
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
            return View(prerequisNiveau);
        }

        // GET: PrerequisNiveaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prerequisNiveau = await _context.PrerequisNiveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prerequisNiveau == null)
            {
                return NotFound();
            }

            return View(prerequisNiveau);
        }

        // POST: PrerequisNiveaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prerequisNiveau = await _context.PrerequisNiveau.FindAsync(id);
            _context.PrerequisNiveau.Remove(prerequisNiveau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrerequisNiveauExists(int id)
        {
            return _context.PrerequisNiveau.Any(e => e.Id == id);
        }

        private bool AlreadyExists(Prerequis prerequis, Niveau niveau)
        {
            return _context.PrerequisNiveau.Any(e => e.Prerequis.Intitule == prerequis.Intitule && e.Niveau.Intitule == niveau.Intitule);
        }
    }
}
