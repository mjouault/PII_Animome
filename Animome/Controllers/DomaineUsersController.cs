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
            if (ModelState.IsValid)
            {
                //var id = viewModel.ApplicationUser.Id;
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                DomaineUser domaineUserAjoute = new DomaineUser
                {
                    Domaine = viewModel.Domaine,
                    ApplicationUser = user
                };

                _context.Add(domaineUserAjoute);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index","ApplicationUsers");
            }
            return View(viewModel);
        }

        // GET: DomaineUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaineUser = await _context.DomaineUser.FindAsync(id);
            if (domaineUser == null)
            {
                return NotFound();
            }
            return View(domaineUser);
        }

        // POST: DomaineUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] DomaineUser domaineUser)
        {
            if (id != domaineUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domaineUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomaineUserExists(domaineUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ApplicationUsers");
            }
            return View(domaineUser);
        }

        // GET: DomaineUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: DomaineUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domaineUser = await _context.DomaineUser.FindAsync(id);
            _context.DomaineUser.Remove(domaineUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomaineUserExists(int id)
        {
            return _context.DomaineUser.Any(e => e.Id == id);
        }
    }
}
