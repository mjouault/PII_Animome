using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Animome.Models;

namespace Animome.Controllers
{
    public class SuiviPrerequisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviPrerequisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviPrerequis
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = from s in _context.SuiviPrerequis select s;

            suiviPrerequis = _context.SuiviPrerequis.Where(x => x.SuiviCompetence.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices);

            return View(await suiviPrerequis.ToListAsync());
        }

        public async Task<IActionResult> AfficherPrerequis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = from s in _context.SuiviPrerequis select s;

            suiviPrerequis = _context.SuiviPrerequis.Where(x => x.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.Prerequis)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.Niveau);

            return View(await suiviPrerequis.ToListAsync());
        }


        // GET: SuiviPrerequis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviPrerequis == null)
            {
                return NotFound();
            }

            return View(suiviPrerequis);
        }

        // GET: SuiviPrerequis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuiviPrerequis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prerequis")] SuiviPrerequis suiviPrerequis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suiviPrerequis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suiviPrerequis);
        }

        // GET: SuiviPrerequis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis.FindAsync(id);
            if (suiviPrerequis == null)
            {
                return NotFound();
            }
            return View(suiviPrerequis);
        }

        // POST: SuiviPrerequis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prerequis")] SuiviPrerequis suiviPrerequis)
        {
            if (id != suiviPrerequis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviPrerequis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviPrerequisExists(suiviPrerequis.Id))
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
            return View(suiviPrerequis);
        }

        // GET: SuiviPrerequis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviPrerequis = await _context.SuiviPrerequis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviPrerequis == null)
            {
                return NotFound();
            }

            return View(suiviPrerequis);
        }

        // POST: SuiviPrerequis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviPrerequis = await _context.SuiviPrerequis.FindAsync(id);
            _context.SuiviPrerequis.Remove(suiviPrerequis);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Patients");
        }

        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviPrerequis1 = _context.SuiviPrerequis.Where(x => x.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence)
                    .ThenInclude(sc => sc.Suivi)
                .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence);

            var suiviPrerequis = await suiviPrerequis1.SingleAsync();


            try
            {
                if (suiviPrerequis.Etat != EtatEnum.e3)
                {
                    suiviPrerequis.Etat = EtatEnum.e3;
                    suiviPrerequis.DateValide = DateTime.Now;

                    foreach (SuiviNiveau sn in suiviPrerequis.LesSuiviNiveaux)
                    {
                        if (sn.Etat != EtatEnum.e3)
                        {
                            sn.Etat = EtatEnum.e3;
                            sn.DateValide = DateTime.Now;

                            foreach (SuiviExercice se in sn.LesSuiviExercices)
                            {
                                if (!se.Valide)
                                {
                                    se.Valide = true;
                                    se.DateValide = DateTime.Now;
                                    _context.Update(se);
                                }
                            }
                        }
                        _context.Update(sn);
                    }
                    _context.Update(suiviPrerequis);
                    await _context.SaveChangesAsync();
                    MajEtats(suiviPrerequis);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviPrerequisExists(suiviPrerequis.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Patients");
        }

        public async Task<IActionResult> AnnulerValidation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


           var  suiviPrerequis1 = _context.SuiviPrerequis.Where(x => x.Id == id)
                .Include(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                 .Include(suiviPrerequis => suiviPrerequis.SuiviCompetence);

            var suiviPrerequis = await suiviPrerequis1.SingleAsync();

            try
            {
                if (suiviPrerequis.Etat != EtatEnum.e1)
                {
                    suiviPrerequis.Etat = EtatEnum.e1;
                    suiviPrerequis.DateValide = DateTime.MinValue;
                   
                    foreach (SuiviNiveau sn in suiviPrerequis.LesSuiviNiveaux)
                    {
                        if (sn.Etat != EtatEnum.e1)
                        {
                            sn.Etat = EtatEnum.e1;
                            sn.DateValide = DateTime.MinValue;

                            foreach (SuiviExercice se in sn.LesSuiviExercices)
                            {
                                if (se.Valide)
                                {
                                    se.Valide = false;
                                    se.DateValide = DateTime.MinValue;
                                    _context.Update(se);
                                }
                            }
                        }
                        _context.Update(sn);
                    }
                    _context.Update(suiviPrerequis);
                    await _context.SaveChangesAsync();
                    MajEtats(suiviPrerequis);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviPrerequisExists(suiviPrerequis.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Patients");
        }

        private bool SuiviPrerequisExists(int id)
        {
            return _context.SuiviPrerequis.Any(e => e.Id == id);
        }

        private async void MajEtats(SuiviPrerequis suiviPrerequis)
        {
            var suiviCompetence = await _context.SuiviCompetence.FindAsync(suiviPrerequis.SuiviCompetence.Id);
            suiviCompetence.Etat = suiviCompetence.EtatMaj();
            _context.Update(suiviCompetence);
            await _context.SaveChangesAsync();

            var suivi = await _context.Suivi.FindAsync(suiviCompetence.Suivi.Id);
            suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
            await _context.SaveChangesAsync();
        }
       
    }
}
