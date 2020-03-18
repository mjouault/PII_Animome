using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;

namespace Animome.Models
{
    public class Suivi
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Domaine Domaine { get; set; }
        public List<SuiviApplicationUser> LesSuiviApplicationUsers { get; set; }

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }
        public EtatEnum Etat { get; set; }
        public DateTime DateValide { get; set; }
    }

    public enum EtatEnum
    {
        [Display(Name = "A faire")]
        e1 = 0,
        [Display(Name = "En cours")]
        e2 = 1,
        [Display(Name = "Validé")]
        e3 = 2
    }
    public class Domaine
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public List<Suivi> LesSuivis { get; set; }

        public Domaine() { }
        public Domaine(string intitule)
        {
            Intitule = intitule;
        }
    }

}
