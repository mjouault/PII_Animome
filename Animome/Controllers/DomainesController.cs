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

namespace Animome.Controllers
{
    [Authorize]
    public class DomainesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DomainesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Domaines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Domaine.OrderBy(x=>x.Intitule).ToListAsync());
        }


        // GET: Domaines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Domaines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule")] Domaine domaine)
        {
            if (AlreadyExists(domaine.Intitule))
            {
                ModelState.AddModelError("Intitule", "Erreur : Existe déjà");
            }

            if (ModelState.IsValid)
            {
                _context.Add(domaine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(domaine);
        }

        // GET: Domaines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaine = await _context.Domaine.FindAsync(id);
            if (domaine == null)
            {
                return NotFound();
            }
            return View(domaine);
        }

        // POST: Domaines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intitule")] Domaine domaine)
        {
            if (id != domaine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domaine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomaineExists(domaine.Id))
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
            return View(domaine);
        }

        // GET: Domaines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaine = await _context.Domaine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaine == null)
            {
                return NotFound();
            }

            return View(domaine);
        }

        // POST: Domaines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domaine = await _context.Domaine.FindAsync(id);
            var suivi = await _context.Suivi.FindAsync(id);

            //Un  programme de suivi étant rattaché à un domaine, la supression d'un domaine entraine la suppression des suivis associés 
            //et de ses composantes (suiviCompetence, suiviPrerequis, suiviNiveaux, suiviExercices)

            var suiviExercicesSupprimes = await _context.SuiviExercice.Where(e => e.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Domaine.Id == id).ToListAsync();
            if (suiviExercicesSupprimes != null)
            {
                foreach (var i in suiviExercicesSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviNiveauxSupprimes = await _context.SuiviNiveau.Where(e => e.SuiviPrerequis.SuiviCompetence.Suivi.Domaine.Id == id).ToListAsync();
            if (suiviNiveauxSupprimes != null)
            {
                foreach (var i in suiviNiveauxSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviPrerequisSupprimes = await _context.SuiviPrerequis.Where(e => e.SuiviCompetence.Suivi.Domaine.Id == id).ToListAsync();
            if (suiviPrerequisSupprimes != null)
            {
                foreach (var i in suiviPrerequisSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviCompetenceSupprimes = await _context.SuiviCompetence.Where(e => e.Suivi.Domaine.Id == id).ToListAsync();
            if (suiviCompetenceSupprimes != null)
            {
                foreach (var i in suiviCompetenceSupprimes)
                {
                    _context.Remove(i);
                }
            }


            var domaineUserSupprimes = await _context.DomaineUser.Where(e => e.Domaine.Id == id).ToListAsync();
            if (domaineUserSupprimes != null)
            {
                foreach (var i in domaineUserSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviSupprimes = await _context.Suivi.Where(e => e.Domaine.Id == id).ToListAsync();
            if (suiviSupprimes != null)
            {
                foreach (var i in suiviSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var domaineCompetenceSupprimes = await _context.DomaineCompetence.Where(e => e.Domaine.Id == id).ToListAsync();
            if (domaineCompetenceSupprimes != null)
            {
                foreach (var i in domaineCompetenceSupprimes)
                {
                    _context.Remove(i);
                }
            }
            _context.Domaine.Remove(domaine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomaineExists(int id)
        {
            return _context.Domaine.Any(e => e.Id == id);
        }

        private bool AlreadyExists(string nom)
        {
            return _context.Domaine.Any(e => e.Intitule== nom);
        }
    }
}
