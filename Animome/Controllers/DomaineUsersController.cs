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
    [Authorize]
    public class DomaineUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DomaineUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DomaineUsers
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return NotFound();
            }

            var domaineUsers = await _context.DomaineUser.Where(x => x.ApplicationUser == user)
                .Include(x=> x.Domaine)
                .ToListAsync();

            ViewData["idUser"] = id;
            return View(domaineUsers);
        }

        // GET: DomaineUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaineUser = await _context.DomaineUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaineUser == null)
            {
                return NotFound();
            }

            return View(domaineUser);
        }

        // GET: DomaineUsers/Create
        public async Task<IActionResult> Create(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            IQueryable<string> DomaineQuery = from x in _context.Domaine
                                              orderby x.Intitule
                                              select x.Intitule;

            var viewModel = new DomaineUserCreateViewModel
            {
                Domaines = new SelectList(await DomaineQuery.Distinct().ToListAsync()),
            };

            ViewData["idUser"] = id;
            return View(viewModel);
        }


        // POST: DomaineUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, DomaineUserCreateViewModel viewModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (AlreadyExists(viewModel.Domaine.Intitule, user))
            {
                ModelState.AddModelError("Intitule", "Erreur : Existe déjà");
            }

            if (ModelState.IsValid)
            {
                var domaine = await _context.Domaine.Where(x=>x.Intitule==viewModel.Domaine.Intitule).SingleOrDefaultAsync();
                DomaineUser domaineUserAjoute = new DomaineUser
                {
                    Domaine = domaine,
                    ApplicationUser = user
                };

                _context.Add(domaineUserAjoute);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index","ApplicationUsers");
            }
            //Permet d'afficher de nouveau les noms des domaines dans les SelectList en cas d'erreur
            else
            {
                IQueryable<string> DomaineQuery = from x in _context.Domaine
                                                  orderby x.Intitule
                                                  select x.Intitule;

                viewModel.Domaines = new SelectList(await DomaineQuery.Distinct().ToListAsync());
                ViewData["idUser"] = id;
            }
            return View(viewModel);
        }


        // GET: DomaineUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaineUser = await _context.DomaineUser
                .Where(x => x.Id == id)
                .Include(x => x.ApplicationUser)
                .SingleAsync();

            if (domaineUser == null)
            {
                return NotFound();
            }

            return View(domaineUser);
        }

        // POST: DomaineUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domaineUser = await _context.DomaineUser.Where(x => x.Id==id)
                .Include(x=>x.ApplicationUser)
                .SingleAsync();
            _context.DomaineUser.Remove(domaineUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "ApplicationUsers", new {domaineUser.ApplicationUser.Id}) ;
        }

        private bool DomaineUserExists(int id)
        {
            return _context.DomaineUser.Any(e => e.Id == id);
        }

        private bool AlreadyExists(string nom, ApplicationUser user)
        {
            return _context.DomaineUser.Any(x => x.Domaine.Intitule == nom && x.ApplicationUser.Id == user.Id);
        }
    }
}
