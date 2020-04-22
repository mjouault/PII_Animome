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

namespace Animome.Models
{
    [Authorize]
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

        // GET: création
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule")] Competence competence)
        {
            if (AlreadyExists(competence))
            {
                ModelState.AddModelError("Intitule", "Erreur : élément déjà existant");
            }

            if (ModelState.IsValid)
            {
                _context.Add(competence);
                await _context.SaveChangesAsync();
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

            var competencePrerequisSupprimes = await _context.CompetencePrerequis.Where(e => e.Competence.Id == id).ToListAsync();
            if (competencePrerequisSupprimes != null)
            {
                foreach (var i in competencePrerequisSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var domaineCompetenceSupprimes = await _context.DomaineCompetence.Where(e => e.Competence.Id == id).ToListAsync();
            if (domaineCompetenceSupprimes != null)
            {
                foreach (var i in domaineCompetenceSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var competence = await _context.Competence.FindAsync(id);
            _context.Competence.Remove(competence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetenceExists(int id)
        {
            return _context.Competence.Any(e => e.Id == id);
        }

        private bool AlreadyExists(Competence c)
        {
            return _context.Competence.Any(e => e.Intitule == c.Intitule);
        }
    }
}
