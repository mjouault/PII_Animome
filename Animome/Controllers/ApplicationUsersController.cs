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
using Microsoft.AspNetCore.Authorization;

namespace Animome.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
          _roleManager = roleManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Include(user => user.LesDomaines)
                    .ThenInclude(x => x.Domaine)
                .OrderBy(x=>x.Role)
                .ToListAsync();

           /* foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                if (role.Count!= 0)
                {
                    user.Role = role[0].ToString();
                }
                await _userManager.UpdateAsync(user);
                await _userManager.UpdateAsync(user);

            }*/
            return View(users);
        }

        [Authorize]
        public async Task<IActionResult> AfficherProfil()
        {
            ApplicationUser user = await _userManager.Users.Where(x=>x.Id== _userManager.GetUserId(User))
                .Include(x=>x.LesDomaines)
                    .ThenInclude(d=>d.Domaine)
                .SingleAsync();

            return View(user);
            //.Include(lesDomainesUsers => lesDomainesUsers.Domaine).ToListAsync());

            /*return View(await _userManager.Userse
                .Include(user => user.LesDomaineUsers)
                .ThenInclude(lesDomainesUsers => lesDomainesUsers.Domaine).ToListAsync());*/
        }

        // GET: ApplicationUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.Users.Where(x => x.Id == id)
                .Include(x => x.LesDomaines)
                    .ThenInclude(d => d.Domaine)
                .SingleOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nom,Prenom,Email")] ApplicationUser applicationUser)
        {
            var user = await _userManager.Users.Where(x=>x.Id==id)
                .Include(x=>x.LesDomaines)
                    .ThenInclude(d=>d.Domaine)
                .SingleOrDefaultAsync();
            try
             {
                user.Nom = applicationUser.Nom;
                user.Prenom = applicationUser.Prenom;
                user.Email = applicationUser.Email;
                //user.PhoneNumber = applicationUser.PhoneNumber;
                await _userManager.UpdateAsync(user);
             }
             catch (DbUpdateConcurrencyException)
             {
                return NotFound();
             }

            return RedirectToAction("Index", "ApplicationUsers");
        }

        public async Task<IActionResult> EditProfil()
        {
           var id=  _userManager.GetUserId(User);
            var user = await _userManager.Users.Where(x => x.Id == id)
                        .Include(x => x.LesDomaines)
                            .ThenInclude(d => d.Domaine)
                        .SingleOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            return View("Edit", user);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfil([Bind("Id,Nom,Prenom,Email")] ApplicationUser applicationUser)
        {
            var id = _userManager.GetUserId(User);
            var user = await _userManager.Users.Where(x => x.Id == id)
                        .Include(x => x.LesDomaines)
                            .ThenInclude(d => d.Domaine)
                        .SingleOrDefaultAsync();
            try
            {
                user.Nom = applicationUser.Nom;
                user.Prenom = applicationUser.Prenom;
                user.Email = applicationUser.Email;
                //user.PhoneNumber = applicationUser.PhoneNumber;
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return RedirectToAction("AfficherProfil", "ApplicationUsers");
        }

        public async Task <IActionResult> Accepter(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "Utilisateur");
            user.Role = "Utilisateur";
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "ApplicationUsers");
        }

        public IActionResult CreerRole()
        {
            return View();
        }

        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> CreerRole ([Bind("Id,Name")] ApplicationRole applicationRole)
         {
            if (ModelState.IsValid)
             {
                 await _roleManager.CreateAsync(applicationRole);
                 return RedirectToAction("AfficherProfil", "ApplicationUsers");
             }
             return View(applicationRole);
         }*/

        // GET: Patients/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var rolesForUser = await _userManager.GetRolesAsync(user);
            var logins = await _userManager.GetLoginsAsync(user);

            using (var transaction = _context.Database.BeginTransaction())
            {
               /* foreach (var login in logins.ToList())
                {
                    await _userManager.RemoveLoginAsync(user,login);
                }*/

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                var suiviApplicationUserSupprimes = await _context.SuiviApplicationUser.Where(e => e.ApplicationUser.Id == id).ToListAsync();
                if (suiviApplicationUserSupprimes != null)
                {
                    foreach (var i in suiviApplicationUserSupprimes)
                    {
                        _context.Remove(i);
                    }
                }

                var patientUserSupprimes = await _context.PatientUser.Where(e => e.ApplicationUser.Id == id).ToListAsync();
                if (patientUserSupprimes != null)
                {
                    foreach (var i in patientUserSupprimes)
                    {
                        _context.Remove(i);
                    }
                }

                await _userManager.DeleteAsync(user);
                transaction.Commit();

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Patients");
            }
        }

        private bool ApplicationUserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
