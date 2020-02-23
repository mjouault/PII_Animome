using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviPrerequis
    {
        public int Id { get; set; }
        public SuiviCompetence SuiviCompetence { get; set; }
        public List<SuiviNiveau> LesNiveaux { get; set; }

        public PrerequisEnum Prerequis {get;set;}
    }

    public enum PrerequisEnum
    {
        [Display(Name = "Prerequis1")]
        p1 = 1,
        [Display(Name = "Prerequis2")]
        p2 = 2,
        [Display(Name = "Prerequis3")]
        p3 = 3,
        [Display(Name = "Prerequis4")]
        p4 = 4
    }
    public class Prerequis
    {
        public int Id { get; set; }

        public string Intitule { get; set; }

        public List <SuiviPrerequis> LesSuiviPrerequis { get; set; }
    }
}
