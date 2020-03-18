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
        //private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           //_roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
                //.Include(user => user.LesDomaineUsers)
                //.ThenInclude(lesDomainesUsers => lesDomainesUsers.Domaine).ToListAsync());
        }

        [Authorize]
        public IActionResult AfficherProfil()
        {

            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userid).Result;

            return View(user);
            //.Include(lesDomainesUsers => lesDomainesUsers.Domaine).ToListAsync());

            /*return View(await _userManager.Users
                .Include(user => user.LesDomaineUsers)
                .ThenInclude(lesDomainesUsers => lesDomainesUsers.Domaine).ToListAsync());*/
        }

        // POST: Domaines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreerDomaine(DomaineCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);

                Suivi suiviAjoute = new Suivi
                {
                    Domaine = viewModel.Suivi.Domaine,
                };

                SuiviApplicationUser suiviApplicationUserAjoute = new SuiviApplicationUser
                {
                    Suivi = suiviAjoute,
                    ApplicationUser = user
                };

                _context.Add(suiviAjoute);
                _context.Add(suiviApplicationUserAjoute);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: ApplicationUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
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
        public async Task<IActionResult> Edit([Bind("Id,Nom,Prenom,Email")] ApplicationUser applicationUser)
        {
            await _userManager.UpdateAsync(applicationUser);
            /* try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!_userManager.ApplicationUserExists(applicationUser.Id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }*/
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}
