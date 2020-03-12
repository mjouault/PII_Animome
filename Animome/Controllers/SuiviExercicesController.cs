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
    public class SuiviExercicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviExercicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviExercices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuiviExercice.ToListAsync());
        }

        // GET: SuiviExercices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviExercice = await _context.SuiviExercice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviExercice == null)
            {
                return NotFound();
            }

            return View(suiviExercice);
        }

        // GET: SuiviExercices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuiviExercices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valide,DateFait,DateValide")] SuiviExercice suiviExercice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suiviExercice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suiviExercice);
        }

        // GET: SuiviExercices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviExercice = await _context.SuiviExercice.FindAsync(id);
            if (suiviExercice == null)
            {
                return NotFound();
            }
            return View(suiviExercice);
        }

        // POST: SuiviExercices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valide,DateFait,DateValide")] SuiviExercice suiviExercice)
        {
            if (id != suiviExercice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviExercice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviExerciceExists(suiviExercice.Id))
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
            return View(suiviExercice);
        }

        // GET: SuiviExercices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviExercice = await _context.SuiviExercice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviExercice == null)
            {
                return NotFound();
            }

            return View(suiviExercice);
        }

        // POST: SuiviExercices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviExercice = await _context.SuiviExercice.FindAsync(id);
            _context.SuiviExercice.Remove(suiviExercice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviExercice = await _context.SuiviExercice.FindAsync(id);
            try
            {
                if (!suiviExercice.Valide)
                {
                    suiviExercice.Valide = true;
                    suiviExercice.DateValide = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviExerciceExists(suiviExercice.Id))
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

            var suiviExercice = await _context.SuiviExercice.FindAsync(id);
            try
            {
                if (!suiviExercice.Valide)
                {
                    suiviExercice.Valide = true;
                    suiviExercice.DateValide = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviExerciceExists(suiviExercice.Id))
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

        private bool SuiviExerciceExists(int id)
        {
            return _context.SuiviExercice.Any(e => e.Id == id);
        }
    }
}
