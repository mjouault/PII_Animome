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
        public DomaineEnum Domaine { get; set; }

        public Domaine Domaine2 { get; set; }
        public List<SuiviApplicationUser> LesSuiviApplicationUsers { get; set; }
        public SuiviExercice DernierExoModifie { get; set; }

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }
    }

    public enum DomaineEnum
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

    public class Domaine
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public List<Suivi> LesSuivis { get; set; }
    }
}
