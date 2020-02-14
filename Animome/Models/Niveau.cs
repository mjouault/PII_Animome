using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Niveau : ElmtSuivi
    {
        public bool Complete { get; set; }
        public List<NiveauPrerequis> LesNiveauxPrerequis;
    }
}
