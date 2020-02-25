using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;
using Animome.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Animome.Controllers
{
    public class SuivisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuivisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Suivis
        public async Task <IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = from s in _context.Suivi select s;

            suivi = _context.Suivi.Where(x => x.Patient.Id==id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx=> lesSuiviNivx.LesSuiviExercices)
                 .Include(suivi => suivi.LesSuiviApplicationUsers)
                      .ThenInclude(lesApplicationUsers => lesApplicationUsers.ApplicationUser);

            ViewData["idPatient"] = id;
            return View(await suivi.ToListAsync());
        }

        // GET: Suivis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suivi == null)
            {
                return NotFound();
            }

            return View(suivi);
        }

        // GET: Suivis/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["idPatient"] = id;
            return View();

        }

        // POST: Suivis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuiviCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = await _context.Patient.FindAsync(viewModel.Patient.Id);
                ApplicationUser user = await _userManager.GetUserAsync(User);

                Suivi suiviAjoute = new Suivi
                {
                    Patient = patient,
                    Domaine = viewModel.Suivi.Domaine,
                };

                SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                {
                    Competence = viewModel.SuiviCompetence.Competence,
                    Suivi = suiviAjoute
                };

                SuiviPrerequis suiviPrerequisAjoute = new SuiviPrerequis
                {
                    Prerequis = viewModel.SuiviPrerequis.Prerequis,
                    SuiviCompetence=suiviCompetenceAjoute,
                };

                SuiviNiveau suiviNiveauAjoute = new SuiviNiveau
                {
                    Niveau = viewModel.SuiviNiveau.Niveau,
                    SuiviPrerequis=suiviPrerequisAjoute,
                };

                SuiviExercice suiviExerciceAjoute = new SuiviExercice
                {
                    Exercice = viewModel.SuiviExercice.Exercice,
                    Fait = false,
                    Valideur = user,
                    SuiviNiveau=suiviNiveauAjoute,
                };

                _context.Add(suiviAjoute);
                _context.Add(suiviCompetenceAjoute);
                _context.Add(suiviPrerequisAjoute);
                _context.Add(suiviNiveauAjoute);
                _context.Add(suiviExerciceAjoute);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = viewModel.Patient.Id });
            }
            return View(viewModel.Suivi.LesSuiviApplicationUsers);
        }

        // GET: Suivis/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(suivi);
        }

        // POST: Suivis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Domaine")] Suivi suivi)
        {
            if (id != suivi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suivi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviExists(suivi.Id))
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
            return View(suivi);
        }

        // GET: Suivis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suivi == null)
            {
                return NotFound();
            }

            return View(suivi);
        }

        // POST: Suivis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suivi = await _context.Suivi.FindAsync(id);
            _context.Suivi.Remove(suivi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuiviExists(int id)
        {
            return _context.Suivi.Any(e => e.Id == id);
        }
    }
}
