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
using Microsoft.AspNetCore.Authorization;

namespace Animome.Controllers
{
    public class SuiviExercicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuiviExercicesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SuiviExercices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuiviExercice.ToListAsync());
        }

        /// <summary>
        /// Permet à un utilisateur de valider/Marquer un exercice comme acquise pour un patient donné
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suiviExercice = await _context.SuiviExercice.Where(x => x.Id == id)
                .Include(se => se.SuiviNiveau)
                    .ThenInclude(sn => sn.SuiviPrerequis)
                        .ThenInclude(sp => sp.SuiviCompetence)
                            .ThenInclude(sc=>sc.Suivi)
                .SingleAsync();
            try
            {
                
                if (!suiviExercice.Valide)
                {
                    suiviExercice.Valide = true;
                    suiviExercice.DateValide = DateTime.Now;
                    suiviExercice.Valideur=  await _userManager.GetUserAsync(User);
                    await _context.SaveChangesAsync();

                    MajEtats(suiviExercice);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviExerciceExists(suiviExercice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new { suiviExercice.SuiviNiveau.SuiviPrerequis.Id });
        }

        public async Task<IActionResult> AnnulerValidation (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var suiviExercice = await _context.SuiviExercice.Where(x => x.Id == id)
                .Include(se => se.SuiviNiveau)
                    .ThenInclude(sn => sn.SuiviPrerequis)
                        .ThenInclude(sp => sp.SuiviCompetence)
                            .ThenInclude(sc => sc.Suivi)
                .SingleAsync();

            try
            {
                if (suiviExercice.Valide)
                {
                    suiviExercice.Valide = false;
                    suiviExercice.DateValide = DateTime.MinValue;

                    MajEtats(suiviExercice);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviExerciceExists(suiviExercice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherPrerequis", "SuiviPrerequis", new { suiviExercice.SuiviNiveau.SuiviPrerequis.Id });
        }

        private bool SuiviExerciceExists(int id)
        {
            return _context.SuiviExercice.Any(e => e.Id == id);
        }

        /// <summary>
        /// Suite au changement d'état d'un suiviExercice, mise à jour en conséquence des éléments de niveaux supérieur (SuiviNiveau, SuiviPrerequis, SuivCompetence, Suivi)
        /// </summary>
        /// <param name="suiviExercice"></param>
        private void MajEtats(SuiviExercice suiviExercice)
        {
            //Maj bdd de l'Etat du suiviNiveau associé
            var suiviNiveau =  _context.SuiviNiveau.Where(x => x.Id == suiviExercice.SuiviNiveau.Id)
                .Include(s => s.LesSuiviExercices)
                .Single();
            suiviNiveau.Etat = suiviNiveau.EtatMaj();
            _context.Update(suiviNiveau);

            //Maj bdd de l'Etat du suiviPrerequis associé
            var suiviPrerequis =  _context.SuiviPrerequis.Where(x => x.Id == suiviNiveau.SuiviPrerequis.Id)
                .Include(s => s.LesSuiviNiveaux)
                .Single();
            suiviPrerequis.Etat = suiviPrerequis.EtatMaj();
            _context.Update(suiviPrerequis);

            //Maj bdd de l'Etat du suiviCompetence associé
            var suiviCompetence =  _context.SuiviCompetence.Where(x => x.Id == suiviPrerequis.SuiviCompetence.Id)
                .Include(s => s.LesSuiviPrerequis)
                .Single();
            suiviCompetence.Etat = suiviCompetence.EtatMaj();
            _context.Update(suiviCompetence);

            //Maj bdd de l'Etat du suivi associé
            var suivi =  _context.Suivi.Where(x => x.Id == suiviCompetence.Suivi.Id)
                .Include(s => s.LesSuiviCompetences)
                .Single();
            suivi.Etat = suivi.EtatMaj();
            _context.Update(suivi);
        }
    }
}
