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

        public Niveau Niveau { get; set; }
        //public NiveauEnum Niveau { get; set; }

        public int Nb { get; set; } //Objectif

        public SuiviNiveau()
        {
            for (int i = 0; i < 5; i++)
            {
                new SuiviExercice
                {
                    SuiviNiveau = this,
                    Fait = false
                };
            }
        }
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

        public List<PrerequisNiveau> LesPrerequisNiveaux { get; set; }

        public Niveau() { }
        public Niveau(string intitule) 
        { 
            Intitule = intitule; 
        }
    }

    public class PrerequisNiveau
    {
        public int Id { get; set; }
        public Niveau Niveau { get; set;}
        public Prerequis Prerequis { get; set; }
    }
}
