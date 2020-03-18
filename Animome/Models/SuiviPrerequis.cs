using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviPrerequis
    {
        public int Id { get; set; }
        public SuiviCompetence SuiviCompetence { get; set; }
        public List<SuiviNiveau> LesSuiviNiveaux { get; set; }
        public Prerequis Prerequis { get; set; }
        public EtatEnum Etat { get; set; }
        public DateTime DateValide { get; set; }
    }

    public class Prerequis
    {
        public int Id { get; set; }

        public string Intitule { get; set; }

        public List <SuiviPrerequis> LesSuiviPrerequis { get; set; }

        public List<CompetencePrerequis> LesCompetencePrerequis { get; set; }

        public Prerequis() { }
        public Prerequis(string intitule) { Intitule = intitule; }
    }

    public class CompetencePrerequis
    {
        public int Id { get; set; }
        public Competence Competence { get; set; }
        public Prerequis Prerequis { get; set; }
    }
}
