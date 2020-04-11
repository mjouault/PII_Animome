using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animome.Models;

namespace Animome.ViewModels
{
    public class SuiviEditViewModel
    {
        public Suivi Suivi { get; set; }
        public SuiviCompetence SuiviCompetence { get; set; }
        public SuiviPrerequis SuiviPrerequis { get; set; }
        public SuiviNiveau SuiviNiveau { get; set; }
    }
}
