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

        public bool Valide { get; set; }

        public DateTime DateFait { get; set; } //Date à laquelle il a été fait
        public DateTime DateValide { get; set; } //Date à laquelle il a été valide
        public ApplicationUser Valideur { get; set; } //Application User qui a validé l'exercice

        public SuiviNiveau SuiviNiveau { get; set; }

        public string Essai { get; set; }
    }
}
