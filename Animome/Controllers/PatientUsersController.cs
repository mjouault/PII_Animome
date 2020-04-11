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

namespace Animome.Controllers
{
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

            var patientUsers = _context.PatientUser.Where(x => x.Patient.Id == id)
                .Include(x => x.ApplicationUser);
                   

            ViewData["idPatient"] = id;
            return View(await patientUsers.ToListAsync());
        }

        // GET: PatientUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientUser = await _context.PatientUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientUser == null)
            {
                return NotFound();
            }

            return View(patientUser);
        }

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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index), patient.Id);
            }
            return View(viewModel);
        }

        // GET: PatientUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientUser = await _context.PatientUser.FindAsync(id);
            if (patientUser == null)
            {
                return NotFound();
            }
            return View(patientUser);
        }

        // POST: PatientUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] PatientUser patientUser)
        {
            if (id != patientUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientUserExists(patientUser.Id))
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
            return View(patientUser);
        }

        // GET: PatientUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientUser = await _context.PatientUser
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var patientUser = await _context.PatientUser.FindAsync(id);
            _context.PatientUser.Remove(patientUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientUserExists(int id)
        {
            return _context.PatientUser.Any(e => e.Id == id);
        }
    }
}
