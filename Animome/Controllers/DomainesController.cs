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
    public class DomainesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DomainesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Domaines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Domaine.ToListAsync());
        }

        // GET: Domaines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaine = await _context.Domaine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaine == null)
            {
                return NotFound();
            }

            return View(domaine);
        }

        // GET: Domaines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Domaines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DomaineCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);

                Domaine domaineToAdd = new Domaine
                {
                    LesDomaines = viewModel.Domaine.LesDomaines,
                    Intitule = viewModel.Domaine.Intitule
                };

                DomaineUser domaineUserToAdd = new DomaineUser
                {
                    Domaine = domaineToAdd,
                    User = user
                };

                _context.Add(domaineToAdd);
                _context.Add(domaineUserToAdd);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Domaines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaine = await _context.Domaine.FindAsync(id);
            if (domaine == null)
            {
                return NotFound();
            }
            return View(domaine);
        }

        // POST: Domaines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LesDomaines,Id,Intitule")] Domaine domaine)
        {
            if (id != domaine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(domaine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DomaineExists(domaine.Id))
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
            return View(domaine);
        }

        // GET: Domaines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domaine = await _context.Domaine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (domaine == null)
            {
                return NotFound();
            }

            return View(domaine);
        }

        // POST: Domaines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domaine = await _context.Domaine.FindAsync(id);
            _context.Domaine.Remove(domaine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomaineExists(int id)
        {
            return _context.Domaine.Any(e => e.Id == id);
        }
    }
}
