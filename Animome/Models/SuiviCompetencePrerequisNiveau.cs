using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class SuiviCompetencePrerequisNiveau
    {
        public int Id { get; set; }
        public SuiviCompetencePrerequis SuiviCompetencePrerequis { get; set; }
        public Niveau Niveau { get; set; }

        public List<SuiviCompetencePrerequisNiveauExercice> LesExercices { get; set; }

    }
}
