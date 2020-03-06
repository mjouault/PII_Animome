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
using System.ComponentModel.DataAnnotations;

namespace Animome.Controllers
{
    public class SuivisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuivisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Suivis
        [Authorize]
        public async Task <IActionResult> Index(int? id) //id patient
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = from s in _context.Suivi select s;

            suivi = _context.Suivi.Where(x => x.Patient.Id==id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx=> lesSuiviNivx.LesSuiviExercices)
                 .Include(suivi => suivi.LesSuiviApplicationUsers)
                      .ThenInclude(lesApplicationUsers => lesApplicationUsers.ApplicationUser);

            ViewData["idPatient"] = id;
            return View(await suivi.ToListAsync());
        }

        // GET: Suivis/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suivi == null)
            {
                return NotFound();
            }

            return View(suivi);
        }

        // GET: Suivis/Create
        [Authorize]
        public async Task <IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<string> DomaineQuery = from x in _context.Domaine
                                              orderby x.Intitule
                                              select x.Intitule;
            IQueryable<string> CompetenceQuery = from x in _context.Competence
                                              orderby x.Intitule
                                              select x.Intitule;
            IQueryable<string> PrerequisQuery = from x in _context.Prerequis
                                              orderby x.Intitule
                                              select x.Intitule;
            IQueryable<string> NiveauQuery = from x in _context.Niveau
                                              orderby x.Intitule
                                              select x.Intitule;

            var viewModel1 = new SelectListViewModel
            {
                Domaines = new SelectList(await DomaineQuery.Distinct().ToListAsync()),
                Competences = new SelectList(await CompetenceQuery.Distinct().ToListAsync()),
                Prerequis = new SelectList(await PrerequisQuery.Distinct().ToListAsync()),
                Niveaux = new SelectList(await NiveauQuery.Distinct().ToListAsync()),
            };

            var viewModel2 = new SuiviCreateViewModel
            {
                SelectListViewModel =  viewModel1,
            };

            ViewData["idPatient"] = id;
            return View(viewModel2);

        }

        // POST: Suivis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuiviCreateViewModel viewModel)
        {
            IQueryable<string> DomaineQuery = from d in _context.Users
                                            orderby d.Prenom
                                            select d.Prenom;

            if (ModelState.IsValid)
            {
                Patient patient = await _context.Patient.FindAsync(viewModel.Patient.Id);
                ApplicationUser user = await _userManager.GetUserAsync(User);

                Suivi suiviAjoute = new Suivi
                {
                    Patient = patient,
                    Domaine = viewModel.Suivi.Domaine,
                };

                SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                {
                    Competence = viewModel.SuiviCompetence.Competence,
                    Suivi = suiviAjoute
                };

                SuiviPrerequis suiviPrerequisAjoute = new SuiviPrerequis
                {
                    Prerequis = viewModel.SuiviPrerequis.Prerequis,
                    SuiviCompetence=suiviCompetenceAjoute,
                };

                SuiviNiveau suiviNiveauAjoute = new SuiviNiveau
                {
                    Niveau = viewModel.SuiviNiveau.Niveau,
                    SuiviPrerequis=suiviPrerequisAjoute,
                };

                SuiviExercice suiviExerciceAjoute = new SuiviExercice
                {
                    Exercice = viewModel.SuiviExercice.Exercice,
                    Fait = false,
                    Valideur = user,
                    SuiviNiveau=suiviNiveauAjoute,
                };

                _context.Add(suiviAjoute);
                _context.Add(suiviCompetenceAjoute);
                _context.Add(suiviPrerequisAjoute);
                _context.Add(suiviNiveauAjoute);
                _context.Add(suiviExerciceAjoute);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = viewModel.Patient.Id });
            }
            return View(viewModel.Suivi.LesSuiviApplicationUsers);
        }

        // GET: Suivis/Edit/5
        [Authorize]
       public async Task<IActionResult> Edit(int? id, SuiviEditViewModel viewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

           viewModel.Suivi = await _context.Suivi.FindAsync(id);
            if (viewModel.Suivi == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

       /* public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           // var suivi = await _context.Suivi.FindAsync(id);
            return View();
        }*/



        // POST: Suivis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(int id, SuiviCreateViewModel viewModel)
        {
            if (id != viewModel.Suivi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("entre ds le modelValid");
                try
                {
                    viewModel.Suivi = await _context.Suivi.FindAsync(id);
                    // var domaine = viewModel.Suivi.Domaine;
                    //  var suivi = await _context.Suivi.FindAsync(viewModel.Suivi.Id);

                    Console.WriteLine("entre ds le try");
                    _context.Update(viewModel.Suivi.Domaine);

                    //_context.Update(viewModel.SuiviPrerequis) ;
                    // _context.Update(viewModel.SuiviExercice);
                   // _context.Update(domaine);
                   // _context.Update(suivi);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("entre ds le catch");
                    if (!SuiviExists(viewModel.Suivi.Id))
                    {
                        Console.WriteLine("pbbbbbb");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("sort");
            return View(viewModel);
        }

        // GET: Suivis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suivi == null)
            {
                return NotFound();
            }

            return View(suivi);
        }

        // POST: Suivis/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suivi = await _context.Suivi.FindAsync(id);
            _context.Suivi.Remove(suivi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuiviExists(int id)
        {
            return _context.Suivi.Any(e => e.Id == id);
        }

        public async Task <IActionResult> AfficherSuivi (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = from s in _context.Suivi select s;

            suivi = _context.Suivi.Where(x => x.Patient.Id == id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                 .Include(suivi => suivi.LesSuiviApplicationUsers)
                      .ThenInclude(lesApplicationUsers => lesApplicationUsers.ApplicationUser);

            ViewData["idPatient"] = id;
            return View(await suivi.ToListAsync());
        }

        public async Task <IActionResult> AfficherDomaine()
        {
            return View(await _context.Domaine.ToListAsync());
        }

        public async Task <IActionResult> TestSelect()
        {
            IQueryable<string> DomaineQuery = from x in _context.Domaine
                                              orderby x.Intitule
                                              select x.Intitule;
            var viewModel = new SelectListViewModel
            {
                Domaines = new SelectList(await DomaineQuery.Distinct().ToListAsync()),
            };
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult TestSelect( SelectListViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.IntituleDomaine))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //GET ajouterCompetence
       /* public async Task<IActionResult> AjouterCompetence(int? id, SuiviCreateViewModel viewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            viewModel.Suivi = await _context.Suivi.FindAsync(id);
            if (viewModel.Suivi == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjouterCompetence(int id, SuiviCreateViewModel viewModel)
        {
            if (id != viewModel.Suivi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var suivi = await _context.Suivi
                .FirstOrDefaultAsync(m => m.Id == id);

                SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                {
                    Competence = viewModel.SuiviCompetence.Competence,
                    Suivi = suivi
                };

                _context.Add(suiviCompetenceAjoute);
                await _context.SaveChangesAsync();
                return RedirectToAction("AfficherSuivi", new { suivi.Patient.Id });
            }
            return View(viewModel);
        }*/
    }
}
