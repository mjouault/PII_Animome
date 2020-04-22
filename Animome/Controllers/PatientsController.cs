using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Animome.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Animome.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Patients
        public async Task<IActionResult> Index (string recherchePatient)
        {
            var patients = from p in _context.Patient select p;

            foreach (var p in patients)
            {
                p.LesSuivis = await _context.Suivi.Where(x => x.Patient == p).ToListAsync();
            }

            //Gestion de la barre de recherche
            if (!string.IsNullOrEmpty(recherchePatient))
            {
                patients = patients.Where(p => (p.Numero).ToString().Contains(recherchePatient));
            }

            PatientIndexViewModel patientRecherche = new PatientIndexViewModel
            {
                Patients = await patients.ToListAsync(),
            };
            return View(patientRecherche);
        }


        // GET: Patients/Create
        [Authorize (Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero")] Patient patient)
        {

            if (AlreadyExists(patient.Numero))
            {
                ModelState.AddModelError("Numero", "Erreur : élément déjà existant"); //Affichage d'un message d'erreur + Modèle considéré comme invalide
            }

            if (ModelState.IsValid )
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();

                PatientUser patientUser = new PatientUser
                {
                    Patient = patient,
                    ApplicationUser =await _userManager.GetUserAsync(User)
                };
                _context.Add(patientUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            var suiviExercicesSupprimes = await _context.SuiviExercice.Where(e => e.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Patient.Id == id).ToListAsync();
            if (suiviExercicesSupprimes != null)
            {
                foreach (var i in suiviExercicesSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviNiveauxSupprimes = await _context.SuiviNiveau.Where(e => e.SuiviPrerequis.SuiviCompetence.Suivi.Patient.Id == id).ToListAsync();
            if (suiviNiveauxSupprimes != null)
            {
                foreach (var i in suiviNiveauxSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviPrerequisSupprimes = await _context.SuiviPrerequis.Where(e => e.SuiviCompetence.Suivi.Patient.Id == id).ToListAsync();
            if (suiviPrerequisSupprimes != null)
            {
                foreach (var i in suiviPrerequisSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviCompetenceSupprimes = await _context.SuiviCompetence.Where(e => e.Suivi.Patient.Id == id).ToListAsync();
            if (suiviCompetenceSupprimes != null)
            {
                foreach (var i in suiviCompetenceSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviSupprimes = await _context.Suivi.Where(e => e.Patient.Id == id).ToListAsync();
            if (suiviSupprimes != null)
            {
                foreach (var i in suiviSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var patientUserSupprimes = await _context.PatientUser.Where(e => e.Patient.Id == id).ToListAsync();
            if (patientUserSupprimes != null)
            {
                foreach (var i in patientUserSupprimes)
                {
                    _context.Remove(i);
                }
            }

            _context.Patient.Remove(patient);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patients");
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }

        private bool AlreadyExists(int numero)
        {
            return _context.Patient.Any(e => e.Numero == numero);
        }
    }
}
