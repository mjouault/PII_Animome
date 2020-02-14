using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Prerequis : ElmtSuivi
    {
        public List<NiveauPrerequis> LesNiveauxPrerequis; //Mettre en ICollection
        public List<CompetencePrerequis> LesCompetencesPrerequis { get; set; }
    }
}
