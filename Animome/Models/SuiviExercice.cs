using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviExercice
    {
        public int Id { get; set; }

        public bool Fait { get; set; }

        public DateTime DateFait { get; set; } //Date à laquelle il a été fait
        public DateTime DateValide { get; set; } //Date à laquelle il a été valide

        public ApplicationUser Valideur { get; set; } //Application User qui a validé l'exercice

        public string Commentaire { get; set; } //Eventuel commentaire sur la validation d'un exo
        public SuiviNiveau SuiviNiveau { get; set; }
        public ExerciceEnum Exercice {get;set;}
    }

    public enum ExerciceEnum
    {
        [Display(Name = "Exercice1")]
        e1 = 1,
        [Display(Name = "Exercice2")]
        e2 = 2,
        [Display(Name = "Exercice3")]
        e3 = 3,
        [Display(Name = "Exercice4")]
        e4 = 4
    }

    public class Exercice
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public List<NiveauExercice> LesNveauExercices { get; set; }
    }

    public class NiveauExercice
    {
        public int Id { get; set; }
        public Niveau Niveau { get; set; }
        public Exercice Exercice { get; set; }
    }
}
