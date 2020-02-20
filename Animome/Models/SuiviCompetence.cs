using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class SuiviCompetence
    {
        public int Id { get; set; }
        public Suivi Suivi { get; set; }
        public Competence Competence {get;set;}

        public List<SuiviCompetencePrerequis> LesPrerequis { get; set; }
    }
}
