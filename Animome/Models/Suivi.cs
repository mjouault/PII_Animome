using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Suivi
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Domaine Domaine { get; set; }

        public List<SuiviApplicationUser> LesSuiviApplicaitonUsers { get; set; }
        public Exercice DernierExoModifie { get; set; }

        public List<SuiviCompetence> LesCompetences { get; set; }
    }
}
