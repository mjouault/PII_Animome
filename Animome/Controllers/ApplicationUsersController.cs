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
    [Authorize] //Nécessite une connexion de l'utilisateur
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context; //Permet requêtes vers BDD
        private readonly UserManager<ApplicationUser> _userManager; // Permet requêtes vers Identity 

        public ApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Affichage des Collaborateurs et de leurs informations
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Include(user => user.LesDomaines) //Récupération des domaines associés
                    .ThenInclude(x => x.Domaine)
                .OrderBy(x=>x.Role)
                .ToListAsync();
            return View(users);
        }

        /// <summary>
        /// Afficage des informations personnelles de l'utilisateur
        /// </summary>
        /// <returns></returns>

        [Authorize (Roles="Admin")] //Accès restreint à l'administrateur
        public async Task<IActionResult> AfficherProfil()
        {
            ApplicationUser user = await _userManager.Users.Where(x=>x.Id== _userManager.GetUserId(User))
                .Include(x=>x.LesDomaines)
                    .ThenInclude(d=>d.Domaine)
                .SingleAsync();

            return View(user);        
        }

       /// <summary>
       /// Modification de ses informations personnelles ou de celles de ses collaborateurs
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [Authorize (Roles ="Admin")]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nom,Prenom,Email")] ApplicationUser applicationUser)
        {
            var user = await _userManager.Users.Where(x => x.Id == id)
                .Include(x => x.LesDomaines)
                    .ThenInclude(d => d.Domaine)
                .SingleOrDefaultAsync();
            try
            {
                user.Nom = applicationUser.Nom;
                user.Prenom = applicationUser.Prenom;
                user.Email = applicationUser.Email;
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            if (await _userManager.GetUserAsync(User) == user)
            {
                return RedirectToAction("AfficherProfil", "ApplicationUsers", new { id });
            }
            else
            {
                return RedirectToAction("Index", "ApplicationUsers");
            }
        }

        /// <summary>
        /// Acceptation par l'administrateur d'un nouvel inscrit pour le reconnaitre en tant qu'utilisateur ou non
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [Authorize (Roles ="Admin")]
        public async Task <IActionResult> Accepter(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "Utilisateur");
            user.Role = "Utilisateur"; //Atribution du rôle à ce nouvel inscrit
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "ApplicationUsers");
        }

        /// <summary>
        /// Suppression d'une personne inscrite sur le site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Patients/Delete/5
        [Authorize (Roles ="Admin")]
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

                if (user == null)
                {
                    return NotFound($"Une erreur est survenue.");
                }

                //La suppression d'un utilisateur entraine la suppression des liens entre lui et ses patients
                var patientUserSupprimes = await _context.PatientUser.Where(e => e.ApplicationUser.Id == id).ToListAsync();
                if (patientUserSupprimes != null)
                {
                    foreach (var i in patientUserSupprimes)
                    {
                        _context.Remove(i);
                    }
                }
                await _context.SaveChangesAsync();

                var result = await _userManager.DeleteAsync(user);
                    var userId = await _userManager.GetUserIdAsync(user);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
                    }

                return RedirectToAction("Index", "ApplicationUsers");
        }
    }
}
