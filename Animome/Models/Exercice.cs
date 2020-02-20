using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Exercice : ElmtSuivi
    {
        public List<SuiviCompetencePrerequisNiveauExercice> LesSuiviCompetencePrerequisNiveauExercices { get; set; }
    }
}
