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
    public class PrerequisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrerequisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prerequis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prerequis.ToListAsync());
        }

        // GET: Prerequis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prerequis = await _context.Prerequis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prerequis == null)
            {
                return NotFound();
            }

            return View(prerequis);
        }

        // GET: Prerequis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prerequis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule")] Prerequis prerequis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prerequis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prerequis);
        }

        // GET: Prerequis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prerequis = await _context.Prerequis.FindAsync(id);
            if (prerequis == null)
            {
                return NotFound();
            }
            return View(prerequis);
        }

        // POST: Prerequis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intitule")] Prerequis prerequis)
        {
            if (id != prerequis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prerequis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrerequisExists(prerequis.Id))
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
            return View(prerequis);
        }

        // GET: Prerequis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prerequis = await _context.Prerequis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prerequis == null)
            {
                return NotFound();
            }

            return View(prerequis);
        }

        // POST: Prerequis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prerequis = await _context.Prerequis.FindAsync(id);
            _context.Prerequis.Remove(prerequis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrerequisExists(int id)
        {
            return _context.Prerequis.Any(e => e.Id == id);
        }
    }
}
