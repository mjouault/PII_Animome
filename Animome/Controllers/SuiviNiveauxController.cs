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

        // GET: SuiviNiveaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviNiveau == null)
            {
                return NotFound();
            }

            return View(suiviNiveau);
        }

        // GET: SuiviNiveaux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuiviNiveaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valide,DateValide")] SuiviNiveau suiviNiveau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suiviNiveau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suiviNiveau);
        }

        // GET: SuiviNiveaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau.FindAsync(id);
            if (suiviNiveau == null)
            {
                return NotFound();
            }
            return View(suiviNiveau);
        }

        // POST: SuiviNiveaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valide,DateValide")] SuiviNiveau suiviNiveau)
        {
            if (id != suiviNiveau.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviNiveau);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(suiviNiveau);
        }

        // GET: SuiviNiveaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviNiveau == null)
            {
                return NotFound();
            }

            return View(suiviNiveau);
        }

        // POST: SuiviNiveaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviNiveau = await _context.SuiviNiveau.FindAsync(id);
            _context.SuiviNiveau.Remove(suiviNiveau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviNiveau1 = _context.SuiviNiveau.Where(x => x.Id == id)
                .Include(suiviNiveau => suiviNiveau.Niveau)
                .Include(suiviNiveau => suiviNiveau.LesSuiviExercices);

            var suiviNiveau = await suiviNiveau1.SingleAsync();
            try
            {
                if (!suiviNiveau.Valide)
                {
                    suiviNiveau.Valide = true;
                    suiviNiveau.DateValide = DateTime.Now;

                    foreach (SuiviExercice se in suiviNiveau.LesSuiviExercices)
                    {
                        se.Valide = true;
                        se.DateValide = DateTime.Now;
                    }
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
            return RedirectToAction("Index", "Patients");
        }

        public async Task<IActionResult> AnnulerValidation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviNiveau1 = _context.SuiviNiveau.Where(x => x.Id == id)
                .Include(suiviNiveau => suiviNiveau.Niveau)
                .Include(suiviNiveau => suiviNiveau.LesSuiviExercices);

            var suiviNiveau = await suiviNiveau1.SingleAsync();
            try
            {
                if (!suiviNiveau.Valide)
                {
                    suiviNiveau.Valide = false;
                    suiviNiveau.DateValide = DateTime.MinValue;

                    foreach (SuiviExercice se in suiviNiveau.LesSuiviExercices)
                    {
                        se.Valide = false;
                        se.DateValide = DateTime.MinValue;
                    }
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
            return RedirectToAction("Index", "Patients");
        }


        private bool SuiviNiveauExists(int id)
        {
            return _context.SuiviNiveau.Any(e => e.Id == id);
        }
    }
}
