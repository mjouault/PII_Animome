using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace Animome.Controllers
{
    [Authorize]
    public class NiveauxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NiveauxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Niveaux
        public async Task<IActionResult> Index()
        {
            return View(await _context.Niveau.OrderBy(x=>x.Intitule).ToListAsync());
        }

      
        // GET: Niveaux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Niveaux/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule")] Niveau niveau)
        {
            if (AlreadyExists(niveau))
            {
                ModelState.AddModelError("Intitule", "Erreur : Existe déjà");
            }

            if (ModelState.IsValid)
            {
                _context.Add(niveau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(niveau);
        }

        // GET: Niveaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveau = await _context.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return NotFound();
            }
            return View(niveau);
        }

        // POST: Niveaux/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intitule")] Niveau niveau)
        {
            if (id != niveau.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(niveau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NiveauExists(niveau.Id))
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
            return View(niveau);
        }

        // GET: Niveaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var niveau = await _context.Niveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (niveau == null)
            {
                return NotFound();
            }

            return View(niveau);
        }

        // POST: Niveaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prerequisNiveauSupprimes = await _context.PrerequisNiveau.Where(e => e.Niveau.Id == id).ToListAsync();
            if (prerequisNiveauSupprimes != null)
            {
                foreach (var i in prerequisNiveauSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var niveau = await _context.Niveau.FindAsync(id);
            _context.Niveau.Remove(niveau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NiveauExists(int id)
        {
            return _context.Niveau.Any(e => e.Id == id);
        }
        private bool AlreadyExists(Niveau niveau)
        {
            return _context.Niveau.Any(e => e.Intitule== niveau.Intitule);
        }
    }
}
