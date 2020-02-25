using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviNiveau
    {
        public int Id { get; set; }
        public SuiviPrerequis SuiviPrerequis { get; set; }
        public List<SuiviExercice> LesSuiviExercices { get; set; }
        public NiveauEnum Niveau { get; set; }

        public int Nb { get; set; } //Objectif
    }

    public enum NiveauEnum
    {
        [Display(Name = "Niveau1")]
        n1 = 1,
        [Display(Name = "Niveau2")]
        n2 = 2,
        [Display(Name = "Niveau3")]
        n3 = 3,
        [Display(Name = "Niveau4")]
        n4 = 4
    }

    public class Niveau
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public List<SuiviNiveau> LesSuiviNiveaux { get; set; }
    }
}
