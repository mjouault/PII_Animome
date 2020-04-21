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


            var suivi = await _context.Suivi.Where(x => x.Patient.Id == id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(suivi => suivi.Domaine)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuviCptces => lesSuviCptces.Competence)
                 .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.Prerequis)
                 .ToListAsync();

            ViewData["idPatient"] = id;
            return View(suivi);
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Patient patient = await _context.Patient.Where(x=>x.Id==id).SingleAsync();
                var listeDomaines = await _context.Domaine.ToListAsync();

                PatientUser patientUserAjoute = new PatientUser
                {
                    Patient = patient,
                    ApplicationUser = await _userManager.GetUserAsync(User)
                };
                _context.Add(patientUserAjoute);

                foreach (Domaine d in listeDomaines)
                {
                    Suivi suiviAjoute = new Suivi
                    {
                        Patient = patient,
                        Domaine = d
                    };
                    _context.Add(suiviAjoute);

                    await _context.SaveChangesAsync();

                    var listeDomaineCompetences = await _context.DomaineCompetence.Where(x => x.Domaine == d)
                        .Include(domaineComp => domaineComp.Competence)
                        .ToListAsync();
                        
                     
                    foreach (DomaineCompetence dc in listeDomaineCompetences)
                    {
                        var uneCompetence = dc.Competence;

                        SuiviCompetence suiviCompetenceAjoute = new SuiviCompetence
                        {
                            Competence = uneCompetence,
                            Suivi = suiviAjoute,
                            Etat = EtatEnum.e1
                        };
                        _context.Add(suiviCompetenceAjoute);
                        await _context.SaveChangesAsync();

                        var listeCompetencePrerequis = await _context.CompetencePrerequis.Where(x => x.Competence == uneCompetence)
                            .Include(compPrerequis => compPrerequis.Prerequis)
                            .ToListAsync();

                        foreach (CompetencePrerequis cp in listeCompetencePrerequis)
                        {
                            var unPrerequis = cp.Prerequis;
                            SuiviPrerequis suiviPrerequisAjoute = new SuiviPrerequis
                            {
                                Prerequis= unPrerequis,
                                SuiviCompetence = suiviCompetenceAjoute,
                                Etat=EtatEnum.e1
                            };
                            _context.Add(suiviPrerequisAjoute);
                            await _context.SaveChangesAsync();

                            var listePrerequisNiveaux = await _context.PrerequisNiveau.Where(x => x.Prerequis == unPrerequis)
                            .Include(prerequisNiv => prerequisNiv.Niveau)
                            .ToListAsync();

                            foreach (PrerequisNiveau pn in listePrerequisNiveaux)
                            {
                                var unNiveau = pn.Niveau;
                                SuiviNiveau suiviNiveauAjoute = new SuiviNiveau
                                {
                                    Niveau = unNiveau,
                                    SuiviPrerequis = suiviPrerequisAjoute,
                                    Etat = EtatEnum.e1
                                };
                                _context.Add(suiviNiveauAjoute);

                                int nbSuiviExercicesDefaut = 5;
                                for (int i = 0; i < nbSuiviExercicesDefaut; i++)
                                {
                                    SuiviExercice suiviExerciceAjoute = new SuiviExercice
                                    {
                                       Valide = false,
                                        SuiviNiveau = suiviNiveauAjoute,
                                    };
                                    _context.Add(suiviExerciceAjoute);
                                }
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                };

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Patients");
            }
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
                try
                {
                    viewModel.Suivi = await _context.Suivi.FindAsync(id);
                    // var domaine = viewModel.Suivi.Domaine;
                    //  var suivi = await _context.Suivi.FindAsync(viewModel.Suivi.Id);

                    _context.Update(viewModel.Suivi.Domaine);

                    //_context.Update(viewModel.SuiviPrerequis) ;
                    // _context.Update(viewModel.SuiviExercice);
                   // _context.Update(domaine);
                   // _context.Update(suivi);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiviExists(viewModel.Suivi.Id))
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

            var suiviExercicesSupprimes = await _context.SuiviExercice.Where(e => e.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Id==id).ToListAsync();
            if (suiviExercicesSupprimes != null)
            {
                foreach (var i in suiviExercicesSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviNiveauxSupprimes = await _context.SuiviNiveau.Where(e => e.SuiviPrerequis.SuiviCompetence.Suivi.Id == id).ToListAsync();
            if (suiviNiveauxSupprimes != null)
            {
                foreach (var i in suiviNiveauxSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviPrerequisSupprimes = await _context.SuiviPrerequis.Where(e => e.SuiviCompetence.Suivi.Id == id).ToListAsync();
            if (suiviPrerequisSupprimes != null)
            {
                foreach (var i in suiviPrerequisSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var suiviCompetenceSupprimes = await _context.SuiviCompetence.Where(e => e.Suivi.Id == id).ToListAsync();
            if (suiviCompetenceSupprimes != null)
            {
                foreach (var i in suiviCompetenceSupprimes)
                {
                    _context.Remove(i);
                }
            }

            var patientId = suivi.Patient.Id;
            _context.Suivi.Remove(suivi);
            await _context.SaveChangesAsync();
            return RedirectToAction("AfficherSuivi", "Suivis", new { patientId});
        }

        public async Task<IActionResult>Valider(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi.Where(x => x.Id == id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(suiviCompetence => suiviCompetence.LesSuiviPrerequis)
                    .ThenInclude(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(suivi => suivi.Patient)
                .SingleOrDefaultAsync();

            try
            {
                if (suivi.Etat != EtatEnum.e3)
                {
                    suivi.Etat = EtatEnum.e3;
                    suivi.DateValide = DateTime.Now;
                    foreach (SuiviCompetence sc in suivi.LesSuiviCompetences)
                    {
                        if (sc.Etat != EtatEnum.e3)
                        {
                            sc.Etat = EtatEnum.e3;
                            sc.DateValide = DateTime.Now;

                            foreach (SuiviPrerequis sp in sc.LesSuiviPrerequis)
                            {
                                if (sp.Etat != EtatEnum.e3)
                                {
                                    sp.Etat = EtatEnum.e3;
                                    sp.DateValide = DateTime.Now;

                                    foreach (SuiviNiveau sn in sp.LesSuiviNiveaux)
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
                                            _context.Update(sn);
                                        }
                                    }
                                }
                                _context.Update(sp);
                            }
                            _context.Update(sc);
                        }
                    }
                    suivi.LesSuiviCompetences = await _context.SuiviCompetence.Where(x => x.Suivi.Id == id).ToListAsync();
                    suivi.Etat = suivi.EtatMaj();
                    _context.Update(suivi);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviExists(suivi.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherSuivi", "Suivis", new { suivi.Patient.Id});
        }

        public async Task<IActionResult> AnnulerValidation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi.Where(x => x.Id == id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(suiviCompetence => suiviCompetence.LesSuiviPrerequis)
                    .ThenInclude(suiviPrerequis => suiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                    .Include(suivi=>suivi.Patient)
                    .SingleOrDefaultAsync();

            try
            {
                if (suivi.Etat != EtatEnum.e1)
                {
                    suivi.Etat = EtatEnum.e1;
                    suivi.DateValide = DateTime.MinValue;
                    foreach (SuiviCompetence sc in suivi.LesSuiviCompetences)
                    {
                        if (sc.Etat != EtatEnum.e1)
                        {
                            sc.Etat = EtatEnum.e1;
                            sc.DateValide = DateTime.MinValue;

                            foreach (SuiviPrerequis sp in sc.LesSuiviPrerequis)
                            {
                                if (sp.Etat != EtatEnum.e1)
                                {
                                    sp.Etat = EtatEnum.e1;
                                    sp.DateValide = DateTime.MinValue;

                                    foreach (SuiviNiveau sn in sp.LesSuiviNiveaux)
                                    {
                                        if (sn.Etat != EtatEnum.e1)
                                        {
                                            sn.Etat = EtatEnum.e1;
                                            sn.DateValide = DateTime.MinValue;
                                            _context.Update(sn);

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
                                    _context.Update(sp);
                                }  
                            }
                            _context.Update(sc);
                        }
                    }

                    suivi.LesSuiviCompetences = await _context.SuiviCompetence.Where(x => x.Suivi.Id == id).ToListAsync();
                    suivi.Etat = suivi.EtatMaj();
                    _context.Update(suivi);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuiviExists(suivi.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("AfficherSuivi", "Suivis", new { suivi.Patient.Id});
        }



        private bool SuiviExists(int id)
        {
            return _context.Suivi.Any(e => e.Id == id);
        }

        public async Task <IActionResult> AfficherApercu (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var suivi = await _context.Suivi.Where(x => x.Patient.Id == id)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.LesSuiviNiveaux)
                    .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                .Include(suivi => suivi.Domaine)
                .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuviCptces => lesSuviCptces.Competence)
                 .Include(suivi => suivi.LesSuiviCompetences)
                    .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                    .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.Prerequis)
                 .Include(x=>x.Patient)
                 .ToListAsync();

            ViewData["idPatient"] = id;
            ViewData["numPatient"] = suivi[0].Patient.Numero;
            return View(suivi);
        }

        public async Task<IActionResult> AfficherSuivi(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suivi = await _context.Suivi.Where(x => x.Patient.Id == id)
                 .Include(suivi => suivi.LesSuiviCompetences)
                     .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                     .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.LesSuiviNiveaux)
                     .ThenInclude(lesSuiviNivx => lesSuiviNivx.LesSuiviExercices)
                 .Include(suivi => suivi.Domaine)
                 .Include(suivi => suivi.LesSuiviCompetences)
                     .ThenInclude(lesSuviCptces => lesSuviCptces.Competence)
                  .Include(suivi => suivi.LesSuiviCompetences)
                     .ThenInclude(lesSuiviCptces => lesSuiviCptces.LesSuiviPrerequis)
                     .ThenInclude(lesSuiviPrerequis => lesSuiviPrerequis.Prerequis)
                  .Include(x=>x.Patient)
                  .ToListAsync();

            ViewData["idPatient"] = id;
            ViewData["numPatient"] = suivi[0].Patient.Numero;
            ViewData["pourcentages"] = CalculerPourcentage(suivi.ToList())[0];
            return View(suivi);
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

        [HttpPost]
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

        private List<double> CalculerPourcentage(List<Suivi> suivi)
        {
            List<double> pourcentages = new List<double>();
            foreach(Suivi s in suivi)
            {
                double nbExercicesValides = _context.SuiviExercice
                    .Count(x => x.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Id == s.Id && x.Valide);
                double nbExercicesTotal = _context.SuiviExercice
                    .Count(x => x.SuiviNiveau.SuiviPrerequis.SuiviCompetence.Suivi.Id == s.Id);

                var d = ((nbExercicesValides / nbExercicesTotal) * 100);

                pourcentages.Add(Math.Round(d));
            }
            return pourcentages;
        }
    }
}
