using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Competence: ElmtSuivi
    {
        public List <CompetencePrerequis> LesCompetencesPrerequis { get; set;}

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }
    }
}
