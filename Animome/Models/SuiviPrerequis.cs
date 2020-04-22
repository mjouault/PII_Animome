using System;
using System.Collections.Generic;
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

        public EtatEnum EtatMaj()
        {
            bool valide = false;
            bool vide = false;
            var premierEtat = LesSuiviNiveaux[0].Etat;

            foreach (var e in LesSuiviNiveaux)
            {
                if (e.Etat == EtatEnum.e3) valide = true;
                if (e.Etat == EtatEnum.e1) vide = true;

                // SuiviPrerequis en cours si l'un des niv est en cours OU s'il a des niveaux valides et vides
                if (e.Etat == EtatEnum.e2 || (valide && vide)) //Maj de l'état des niveaux qui le composent
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

    public class Prerequis
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public string Intitule { get; set; }

        public List <SuiviPrerequis> LesSuiviPrerequis { get; set; }

        public List<CompetencePrerequis> LesCompetencePrerequis { get; set; }

        public Prerequis() { }
        public Prerequis(string intitule) { Intitule = intitule; }
    }

    public class CompetencePrerequis
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public Competence Competence { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public Prerequis Prerequis { get; set; }
    }
}
