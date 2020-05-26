using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Animome.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Notes
        public async Task<IActionResult> Index(int? id) //idPatient
        {

            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Note.Where(c => c.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Patient.Id == id)
                 .Include(x => x.ApplicationUser)
                 .Include(x=>x.SuiviNiveau)
                    .ThenInclude(x=>x.Niveau)
                 .Include(x => x.SuiviNiveau.SuiviPrerequis)
                    .ThenInclude(x => x.Prerequis)
                 .Include(x => x.SuiviNiveau.SuiviPrerequis.SuiviCompetence)
                    .ThenInclude(x => x.Competence)
                  .Include(x => x.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi)
                    .ThenInclude(x=>x.Domaine)
                 .ToListAsync();

            ViewData["idPatient"] = id;
            return View(notes);
        }


        // GET: Notes/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviN = await _context.SuiviNiveau.FindAsync(id);
            if (suiviN == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Notes/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Date, SuiviNiveau, ApplicationUser,Texte")]Note note)
        {

            if (ModelState.IsValid)
            {
                var suiviNiveau = await _context.SuiviNiveau.Where(m => m.Id == id)
                    .Include(s=>s.SuiviPrerequis)
                    .FirstOrDefaultAsync();

                ViewData["idSuiviPrerequis"] = suiviNiveau.SuiviPrerequis.Id;

                note.SuiviNiveau = suiviNiveau;
                note.ApplicationUser = await _userManager.GetUserAsync(User);
                note.Date = DateTime.Now;
                _context.Add(note);

                await _context.SaveChangesAsync();
                return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new {suiviNiveau.SuiviPrerequis.Id});
            }

            return View(note);
        }
        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Texte")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    note.Date = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var note2= await _context.Note.FindAsync(id);
                return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new {note2.SuiviNiveau.SuiviPrerequis.Id});
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.Where(x=>x.Id==id)
                .Include(x=>x.SuiviNiveau)
                .ThenInclude(x => x.SuiviPrerequis)
                .SingleAsync();

            var pId = note.SuiviNiveau.SuiviPrerequis.Id;
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new {pId});
        }

        public async Task<IActionResult> NotesPatient (int? id) //idPatient
        {

            if (id == null)
            {
                return NotFound();
            }
            var suivi = await _context.Suivi.Where(c => c.Id == id)
                .Include(x => x.Patient)
                .SingleAsync();

            var notes = await _context.Note.Where(c => c.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Patient.Id == id)
                .Include(x => x.ApplicationUser)
                .ToListAsync();


            ViewData["idPatient"] = suivi.Patient.Id;
            ViewData["idSuivi"] = id;
            return View(notes);
        }


        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}
