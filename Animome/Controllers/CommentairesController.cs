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
    public class CommentairesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentairesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Commentaires
        public async Task<IActionResult> Index(int? id) //idSuivi
        {
            if (id == null)
            {
                return NotFound();
            }
            var commentaires = await _context.Commentaire.Where(c => c.SuiviApplicationUser.Suivi.Id == id)
                .Include(x=>x.SuiviApplicationUser)
                    .ThenInclude(sa=>sa.Suivi)
                        .ThenInclude(s=>s.Patient)
                .ToListAsync();

            ViewData["idPatient"] = commentaires[0].SuiviApplicationUser.Suivi.Patient.Id;
            ViewData["idSuivi"] = id;
            return View(commentaires);
        }

        // GET: Commentaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentaire == null)
            {
                return NotFound();
            }

            return View(commentaire);
        }

        // GET: Commentaires/Create
        public async Task<IActionResult> Create(int? id)
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

        // POST: Commentaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Date, SuiviApplicationUser,Texte")]Commentaire commentaire)
        {

            if (ModelState.IsValid)
            {
                var suiviApplicationUser = await _context.SuiviApplicationUser.FirstOrDefaultAsync(m => m.Suivi.Id == id);
                commentaire.SuiviApplicationUser = suiviApplicationUser;
                commentaire.Date = DateTime.Now;
                _context.Add(commentaire);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Patients");
            }
            return View(commentaire);
        }

        // GET: Commentaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaire.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }
            return View(commentaire);
        }

        // POST: Commentaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Texte")] Commentaire commentaire)
        {
            if (id != commentaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentaireExists(commentaire.Id))
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
            return View(commentaire);
        }

        // GET: Commentaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentaire = await _context.Commentaire
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentaire == null)
            {
                return NotFound();
            }

            return View(commentaire);
        }

        // POST: Commentaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentaire = await _context.Commentaire.FindAsync(id);
            _context.Commentaire.Remove(commentaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentaireExists(int id)
        {
            return _context.Commentaire.Any(e => e.Id == id);
        }
    }
}
