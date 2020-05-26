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
using Animome.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Animome.Controllers
{
    [Authorize (Roles="Admin, Utilisateur")]
    public class PatientUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PatientUsers
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientUsers = await _context.PatientUser.Where(x => x.Patient.Id == id)
                .Include(x => x.ApplicationUser)
                    .ThenInclude(x=>x.LesDomaines)
                .Include(x=>x.Patient)
                .ToListAsync();

            foreach (var p in patientUsers)
            {
                foreach (var d in p.ApplicationUser.LesDomaines)
                {
                    var domainesUser = await _context.DomaineUser.Where(x => x.Id == d.Id).ToListAsync();
                }
            }
            ViewData["idPatient"] = id;
            return View( patientUsers);
        }


        [Authorize(Roles ="Admin")] // seul l'administrateur peut affecter une équipe thérapeutique à un patient
        // GET: PatientUsers/Create
        public async Task<IActionResult> Create(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);
           
            if (patient == null)
            {
                return NotFound();
            }

            IQueryable<string> usersQuery = from x in _userManager.Users
                                              orderby x.Nom
                                              select x.Nom;

            var viewModel = new PatientUserCreateViewModel
            {
                ListeUsers = new SelectList(await usersQuery.Distinct().ToListAsync()),
            };


            ViewData["idPatient"] = id;
            return View(viewModel);
        }

        // POST: PatientUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, PatientUserCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var patient = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);
                var user = await _userManager.Users.Where(x=>x.Nom==viewModel.NomUser).SingleOrDefaultAsync();

                PatientUser nvPatientUser = new PatientUser
                {
                    Patient = patient,
                    ApplicationUser = user

                };
                _context.Add(nvPatientUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { patient.Id });
            }
            //Permet d'afficher de nouveau le contenu de la SelectList en cas d'erreur
            else
            {
                IQueryable<string> usersQuery = from x in _userManager.Users
                                                orderby x.Nom
                                                select x.Nom;

                 viewModel.ListeUsers = new SelectList(await usersQuery.Distinct().ToListAsync());
            }

            ViewData["idPatient"] = id;
            return View(viewModel);
        }


        // GET: PatientUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientUser = await _context.PatientUser.Where(m => m.Id == id)
                .Include(x=>x.Patient)
                .FirstOrDefaultAsync();

            if (patientUser == null)
            {
                return NotFound();
            }

            return View(patientUser);
        }

        // POST: PatientUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientUser = await _context.PatientUser.Where(m => m.Id == id)
                 .Include(x => x.Patient)
                 .FirstOrDefaultAsync();

            var patientId = patientUser.Patient.Id;
            ViewData["idPatient"] = patientId;

            _context.PatientUser.Remove(patientUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {id=patientId});
        }

        private bool PatientUserExists(int id)
        {
            return _context.PatientUser.Any(e => e.Id == id);
        }
    }
}
