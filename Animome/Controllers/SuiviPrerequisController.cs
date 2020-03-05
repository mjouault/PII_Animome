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
    public class SuiviPrerequisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviPrerequisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviPrerequis
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = from s in _context.SuiviPrerequis select s;

            suiviPrerequis = _context.SuiviPrerequis.Where(x => x.SuiviCompetence.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices);

            return View(await suiviPrerequis.ToListAsync());
        }

        public async Task<IActionResult> AfficherPrerequis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = from s in _context.SuiviPrerequis select s;

            suiviPrerequis = _context.SuiviPrerequis.Where(x => x.SuiviCompetence.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices);

            return View(await suiviPrerequis.ToListAsync());
        }


        // GET: SuiviPrerequis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviPrerequis == null)
            {
                return NotFound();
            }

            return View(suiviPrerequis);
        }

        // GET: SuiviPrerequis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuiviPrerequis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prerequis")] SuiviPrerequis suiviPrerequis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suiviPrerequis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suiviPrerequis);
        }

        // GET: SuiviPrerequis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis.FindAsync(id);
            if (suiviPrerequis == null)
            {
                return NotFound();
            }
            return View(suiviPrerequis);
        }

        // POST: SuiviPrerequis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prerequis")] SuiviPrerequis suiviPrerequis)
        {
            if (id != suiviPrerequis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviPrerequis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviPrerequisExists(suiviPrerequis.Id))
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
            return View(suiviPrerequis);
        }

        // GET: SuiviPrerequis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviPrerequis == null)
            {
                return NotFound();
            }

            return View(suiviPrerequis);
        }

        // POST: SuiviPrerequis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviPrerequis = await _context.SuiviPrerequis.FindAsync(id);
            _context.SuiviPrerequis.Remove(suiviPrerequis);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patients");
        }

        private bool SuiviPrerequisExists(int id)
        {
            return _context.SuiviPrerequis.Any(e => e.Id == id);
        }
    }
}
