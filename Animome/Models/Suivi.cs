using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
 
    public class Suivi
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Domaine Domaine { get; set; } // Un patient a un programme de suivi par domaine

        public List<SuiviCompetence> LesSuiviCompetences { get; set; }
        public EtatEnum Etat { get; set; } //Non acquis (e1), en cours d'acquisition (e2) ou validé (e3)
        public DateTime DateValide { get; set; } //Date de validation d'un suivi

        /// <summary>
        /// Maj de l'état d'un suivi suite à l'actualisation de l'état d'un élément qui le compose 
        /// </summary>
        /// <returns></returns>
        public EtatEnum EtatMaj()
        {
            bool valide = false;
            bool vide = false;
            var premierEtat = LesSuiviCompetences[0].Etat;

            //L'état d'un élément dépend de celui des éléments du niveau inférieur qui le composent
            foreach (var e in LesSuiviCompetences)
            {
                if (e.Etat == EtatEnum.e3) valide = true;
                if (e.Etat == EtatEnum.e1) vide = true;

                // si un élément qui le compose est en cours (e2) ou si 
                if (e.Etat == EtatEnum.e2 || (valide && vide))
                {
                    Etat = EtatEnum.e2;
                    return Etat;
                }
            }

            //il n'est pas en cours donc il  est soit vide soit valide 
            //--> Il a le même état que la 1ere compétence qui le compose
            Etat = premierEtat;
            return Etat;
        }
    }

    public enum EtatEnum
    {
        [Display(Name = "A faire")]
        e1 = 0,
        [Display(Name = "En cours")]
        e2 = 1,
        [Display(Name = "Validé")]
        e3 = 2
    }
    public class Domaine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public string Intitule { get; set; }

        public List<Suivi> LesSuivis { get; set; }

        public Domaine() { }
        public Domaine(string intitule)
        {
            Intitule = intitule;
        }
    }

}
