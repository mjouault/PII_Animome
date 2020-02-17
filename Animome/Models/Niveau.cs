using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Niveau : ElmtSuivi
    {
        public List<NiveauPrerequis> LesNiveauxPrerequis;
        public List<PatientNiveau> LesPatientsNiveaux;
    }
}
