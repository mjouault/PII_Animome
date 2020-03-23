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
    public class SuiviNiveauxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuiviNiveauxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SuiviNiveaux
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuiviNiveau.ToListAsync());
        }

        // GET: SuiviNiveaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviNiveau == null)
            {
                return NotFound();
            }

            return View(suiviNiveau);
        }

        // GET: SuiviNiveaux/Create
       /* public async Task <IActionResult> Create(int? id)
        {
            if(id!=null)
            {
                var suiviNiveau = await _context.SuiviNiveau.FindAsync(id);
                if (suiviNiveau != null)
                {
                    SuiviExercice suiviExerciceAjoute = new SuiviExercice
                    {
                        SuiviNiveau = suiviNiveau,
                        Valide = false
                    };
                    _context.Add(suiviExerciceAjoute);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Patients");
                }
            }
            return NotFound();
        }*/

        // POST: SuiviNiveaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       /* public async Task<IActionResult> Create([Bind("Id,Valide,DateValide")] SuiviNiveau suiviNiveau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suiviNiveau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suiviNiveau);
        }*/

        // GET: SuiviNiveaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau.FindAsync(id);
            if (suiviNiveau == null)
            {
                return NotFound();
            }
            return View(suiviNiveau);
        }

        // POST: SuiviNiveaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valide,DateValide")] SuiviNiveau suiviNiveau)
        {
            if (id != suiviNiveau.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suiviNiveau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviNiveauExists(suiviNiveau.Id))
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
            return View(suiviNiveau);
        }

        // GET: SuiviNiveaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviNiveau = await _context.SuiviNiveau
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suiviNiveau == null)
            {
                return NotFound();
            }

            return View(suiviNiveau);
        }

        // POST: SuiviNiveaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suiviNiveau = await _context.SuiviNiveau.FindAsync(id);
            _context.SuiviNiveau.Remove(suiviNiveau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviNiveau1 = _context.SuiviNiveau.Where(x => x.Id == id)
                .Include(suiviNiveau => suiviNiveau.Niveau)
                .Include(suiviNiveau => suiviNiveau.LesSuiviExercices);

            var suiviNiveau = await suiviNiveau1.SingleAsync();
            try
            {
                if (suiviNiveau.Etat != EtatEnum.e3)
                {
                    suiviNiveau.Etat = EtatEnum.e3;
                    suiviNiveau.DateValide = DateTime.Now;

                    foreach (SuiviExercice se in suiviNiveau.LesSuiviExercices)
                    {
                        if (!se.Valide)
                        {
                            se.Valide = true;
                            se.DateValide = DateTime.Now;
                        }
                    }
                    _context.Update(suiviNiveau);
                    await _context.SaveChangesAsync();
                    MajEtats(suiviNiveau);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviNiveauExists(suiviNiveau.Id))
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

        public async Task<IActionResult> AnnulerValidation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var suiviNiveau1 = _context.SuiviNiveau.Where(x => x.Id == id)
                .Include(suiviNiveau => suiviNiveau.Niveau)
                .Include(suiviNiveau => suiviNiveau.LesSuiviExercices);

            var suiviNiveau = await suiviNiveau1.SingleAsync();
            try
            {
                if (suiviNiveau.Etat != EtatEnum.e1)
                {
                    suiviNiveau.Etat = EtatEnum.e1;
                    suiviNiveau.DateValide = DateTime.MinValue;

                    foreach (SuiviExercice se in suiviNiveau.LesSuiviExercices)
                    {
                        if (se.Valide)
                        {
                            se.Valide = false;
                            se.DateValide = DateTime.MinValue;
                            _context.Update(se);
                        }
                    }
                    _context.Update(suiviNiveau);
                    await _context.SaveChangesAsync();
                    MajEtats(suiviNiveau);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviNiveauExists(suiviNiveau.Id))
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


        private bool SuiviNiveauExists(int id)
        {
            return _context.SuiviNiveau.Any(e => e.Id == id);
        }

        /// <summary>
        /// Maj des Etats des éléments de suivi du dessus
        /// </summary>
        /// <param name="suiviNiveau"></param>
        private async void MajEtats(SuiviNiveau suiviNiveau)
        {
            var suiviPrerequis = await _context.SuiviPrerequis.Where(x => x.Id == suiviNiveau.SuiviPrerequis.SuiviCompetence.Id)
                       .Include(s => s.LesSuiviNiveaux)
                       .SingleAsync();

            suiviPrerequis.Etat = suiviPrerequis.EtatMaj();
            _context.Update(suiviPrerequis);
            await _context.SaveChangesAsync();

           var  suiviCompetence = await _context.SuiviCompetence.Where(x => x.Id == suiviPrerequis.SuiviCompetence.Id)
                .Include(s => s.LesSuiviPrerequis)
                .SingleAsync();
            suiviCompetence.Etat = suiviCompetence.EtatMaj();
            _context.Update(suiviCompetence);
            await _context.SaveChangesAsync();

           var  suivi = await _context.Suivi.Where(x => x.Id == suiviCompetence.Suivi.Id)
                .Include(s => s.LesSuiviCompetences)
                .SingleAsync();
            suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
            await _context.SaveChangesAsync();
        }
    }
}
