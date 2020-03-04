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
            return View(await _context.DomaineCompetence.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: DomaineCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] DomaineCompetence domaineCompetence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(domaineCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(domaineCompetence);
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
    }
}
