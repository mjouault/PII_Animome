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
    public class SuiviExercicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuiviExercicesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<IActionResult> Create(int? id)
        {
            if (id != null)
            {
                var suiviNiveau = await _context.SuiviNiveau.FindAsync(id);
                if (suiviNiveau != null)
                {
                    SuiviExercice suiviExerciceAjoute = new SuiviExercice
                    {
                        SuiviNiveau = suiviNiveau,
                        Valide = false
                    };
                    _context.Add(suiviExerciceAjoute);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Patients");
                }
            }
            return NotFound();
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

        [Authorize]
        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviExercice = await _context.SuiviExercice.Where(x => x.Id == id)
                .Include(se => se.SuiviNiveau)
                    .ThenInclude(sn => sn.SuiviPrerequis)
                        .ThenInclude(sp => sp.SuiviCompetence)
                            .ThenInclude(sc=>sc.Suivi)
                .SingleAsync();
            try
            {
                
                if (!suiviExercice.Valide)
                {
                    suiviExercice.Valide = true;
                    suiviExercice.DateValide = DateTime.Now;
                    suiviExercice.Valideur=  await _userManager.GetUserAsync(User);
                    await _context.SaveChangesAsync();
                    MajEtats(suiviExercice);
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
                if (suiviExercice.Valide)
                {
                    suiviExercice.Valide = false;
                    suiviExercice.DateValide = DateTime.MinValue;
                    await _context.SaveChangesAsync();
                    MajEtats(suiviExercice);
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

        private async void MajEtats(SuiviExercice suiviExercice)
        {
            //Maj bdd de l'Etat du suiviNiveau associé
            var suiviNiveau = await _context.SuiviNiveau.Where(x => x.Id == suiviExercice.SuiviNiveau.Id)
                .Include(s => s.LesSuiviExercices)
                .SingleAsync();
            suiviNiveau.Etat = suiviNiveau.EtatMaj();
            _context.Update(suiviNiveau);
            await _context.SaveChangesAsync();

            //Maj bdd de l'Etat du suiviPrerequis associé
            var suiviPrerequis = await _context.SuiviPrerequis.Where(x => x.Id == suiviNiveau.SuiviPrerequis.Id)
                .Include(s => s.LesSuiviNiveaux)
                .SingleAsync();
            suiviPrerequis.Etat = suiviPrerequis.EtatMaj();
            _context.Update(suiviPrerequis);
            await _context.SaveChangesAsync();

            //Maj bdd de l'Etat du suiviCompetence associé
            var suiviCompetence = await _context.SuiviCompetence.Where(x => x.Id == suiviPrerequis.SuiviCompetence.Id)
                .Include(s => s.LesSuiviPrerequis)
                .SingleAsync();
            suiviCompetence.Etat = suiviCompetence.EtatMaj();
            _context.Update(suiviCompetence);
            await _context.SaveChangesAsync();

            //Maj bdd de l'Etat du suivi associé
            var suivi = await _context.Suivi.Where(x => x.Id == suiviCompetence.Suivi.Id)
                .Include(s => s.LesSuiviCompetences)
                .SingleAsync();
            suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
            await _context.SaveChangesAsync();
        }
    }
}
