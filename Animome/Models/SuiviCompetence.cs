using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviCompetence
    {
        public int Id { get; set; }
        public Suivi Suivi { get; set; }

        public Competence Competence { get; set; }
        public List<SuiviPrerequis> LesSuiviPrerequis { get; set; }
        public EtatEnum Etat { get; set; }
        public DateTime DateValide { get; set; }
    }

    public class Competence
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }

        public List<DomaineCompetence> LesDomaineCompetences { get; set; }

        public Competence() { }
        public Competence(string intitule) { Intitule = intitule; }
    }

    public class DomaineCompetence
    {
        public int Id { get; set; }
        public Domaine Domaine { get; set; }
        public Competence Competence { get; set; }
    }

}

