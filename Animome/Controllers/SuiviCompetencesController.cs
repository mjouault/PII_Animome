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
    public class SuiviCompetencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviCompetencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviCompetences
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuiviCompetence.ToListAsync());
        }

        // GET: SuiviCompetences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviCompetence == null)
            {
                return NotFound();
            }

            return View(suiviCompetence);
        }

        // GET: SuiviCompetences/Create
        public async Task <IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi.FindAsync(id);
            if (suivi == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: SuiviCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,Suivi,Competence")] SuiviCompetence suiviCompetence)
        {
              var suivi = await _context.Suivi
               .FirstOrDefaultAsync(m => m.Id == id);

                SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                {
                    Competence = suiviCompetence.Competence,
                    Suivi = suivi
                };
                _context.Add(suiviCompetenceAjoute);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patients");
            //return RedirectToAction("AfficherSuivi", "Suivis", new { suivi.Patient.Id });
        }


        // GET: SuiviCompetences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence.FindAsync(id);
            if (suiviCompetence == null)
            {
                return NotFound();
            }
            return View(suiviCompetence);
        }

        // POST: SuiviCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Competence")] SuiviCompetence suiviCompetence)
        {
            if (id != suiviCompetence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviCompetenceExists(suiviCompetence.Id))
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
            return View(suiviCompetence);
        }

        // GET: SuiviCompetences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviCompetence = await _context.SuiviCompetence
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviCompetence == null)
            {
                return NotFound();
            }

            return View(suiviCompetence);
        }

        // POST: SuiviCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviCompetence = await _context.SuiviCompetence.FindAsync(id);
            _context.SuiviCompetence.Remove(suiviCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patients");
            //return RedirectToAction("AfficherSuivi", "Suivis", new { suiviCompetence.Suivi.Patient.Id });
        }

        private bool SuiviCompetenceExists(int id)
        {
            return _context.SuiviCompetence.Any(e => e.Id == id);
        }
    }
}
