using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class SuiviCompetencePrerequis
    {
        public int Id { get; set; }
        public Prerequis Prerequis { get; set; }
        public SuiviCompetence SuiviCompetence { get; set; }
        public List<SuiviCompetencePrerequisNiveau> LesNiveaux { get; set; }
    }
}
