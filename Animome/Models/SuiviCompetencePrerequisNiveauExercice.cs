using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class SuiviCompetencePrerequisNiveauExercice
    {
        public int Id { get; set; }
        public int Nb { get; set; } //Objectif
        public int NbFaits { get; set; } //Faits

        public DateTime DateValidation { get; set; } //Date à laquelle il a été fait

        public ApplicationUser Valideur { get; set; } //Application User qui a validé l'exercice

        public string Commentaire { get; set; } //Eventuel commentaire sur la validation d'un exo
        public SuiviCompetencePrerequisNiveau SuiviCompetencePrerequisNiveau { get; set; }
        public Exercice Exercice {get;set;}
    }
}
