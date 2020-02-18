using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class Domaine : ElmtSuivi
    {
        public List<DomaineUser> LesDomaineUsers { get; set; }
        public Domaines LesDomaines { get; set; }
    }

    public enum Domaines
    {
        [Display(Name="Motricité globale")]
        motriciteGlobale = 1,
        [Display(Name = "Motricité fine")]
        motriciteFine = 2,
        [Display(Name = "Langage")]
        langage = 3,
        [Display(Name = "Habiletés sociales")]
        habilitesSociales = 4
    }
}
