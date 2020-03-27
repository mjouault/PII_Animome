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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users
                .Include(user => user.LesDomaines)
                    .ThenInclude(x => x.Domaine)
                .ToListAsync());
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

            /*return View(await _userManager.Users
                .Include(user => user.LesDomaineUsers)
                .ThenInclude(lesDomainesUsers => lesDomainesUsers.Domaine).ToListAsync());*/
        }

        // GET: ApplicationUsers/Edit/5
        [Authorize]
        public async Task<IActionResult> EditProfil(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
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
        public async Task<IActionResult> EditProfil(string id, [Bind("Id,Nom,Prenom,Email")] ApplicationUser applicationUser)
        {
            var user = await _userManager.FindByIdAsync(id);
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

        public async Task<IActionResult> Edit(string id)
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
            var user = await _userManager.GetUserAsync(User);
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
