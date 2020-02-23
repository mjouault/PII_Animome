using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class Suivi
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Domaine Domaine { get; set; }

        public List<SuiviApplicationUser> LesSuiviApplicationUsers { get; set; }
        public SuiviExercice DernierExoModifie { get; set; }

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }
    }

    public enum Domaine
    {
        [Display(Name = "Motricité globale")]
        d1 = 1,
        [Display(Name = "Motricité fine")]
        d2 = 2,
        [Display(Name = "Langage")]
        d3 = 3,
        [Display(Name = "Habiletés sociales")]
        d4 = 4
    }
}
