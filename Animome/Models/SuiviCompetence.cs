using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviCompetence
    {
        public int Id { get; set; }
        public Suivi Suivi { get; set; }
        public CompetenceEnum Competence {get;set;}

        public List<SuiviPrerequis> LesSuiviPrerequis { get; set; }
    }
    public enum CompetenceEnum
    {
        [Display(Name = "Competence1")]
        c1 = 1,
        [Display(Name = "Competence2")]
        c2 = 2,
        [Display(Name = "Competence3")]
        c3 = 3,
        [Display(Name = "Competence4")]
        c4 = 4
    }

    public class Competence
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }
    }

}

