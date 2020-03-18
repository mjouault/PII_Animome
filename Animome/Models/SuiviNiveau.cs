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
        public EtatEnum Etat { get; set; }
        public DateTime DateValide { get; set; }
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
