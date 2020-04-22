using System;
using System.Collections.Generic;
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

        public EtatEnum EtatMaj()
        {
            bool valide = false;
            bool vide = false;
            var premierEtat = LesSuiviPrerequis[0].Etat;

            foreach (var e in LesSuiviPrerequis)
            {
                if (e.Etat == EtatEnum.e3) valide = true;
                if (e.Etat == EtatEnum.e1) vide = true;

                if (e.Etat == EtatEnum.e2 || (valide && vide))
                {
                    Etat = EtatEnum.e2;
                    return Etat;
                }
            }

            //il n'est pas en cours donc il  est soit vide soit valide 
            //--> Il a le même état que le premierPrérequis qui le compose
            Etat = premierEtat;
            return Etat;
        }
    }

    public class Competence
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public string Intitule { get; set; }

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }

        public List<DomaineCompetence> LesDomaineCompetences { get; set; }

        public Competence() { }
        public Competence(string intitule) { Intitule = intitule; }
    }

    public class DomaineCompetence
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez choisir un champ")]
        public Domaine Domaine { get; set; }

        [Required(ErrorMessage = "Veuillez choisir un champ")]
        public Competence Competence { get; set; }
    }

}

