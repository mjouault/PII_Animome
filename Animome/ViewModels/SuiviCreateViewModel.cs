using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animome.Models;
using Animome.ViewModels;

namespace Animome.ViewModels
{
    public class SuiviCreateViewModel
    {
        public Patient Patient { get; set; }
        public Suivi Suivi { get; set; }
        public SuiviCompetence SuiviCompetence { get; set; }
        public SuiviPrerequis SuiviPrerequis { get; set; }
        public SuiviNiveau SuiviNiveau { get; set; }
        public SuiviExercice SuiviExercice { get; set; }

        public Competence Competence { get; set; }
        public Domaine Domaine { get; set; }
        public SelectListViewModel  SelectListViewModel {get;set;}

    }
}
