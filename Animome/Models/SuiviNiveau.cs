﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class SuiviNiveau
    {
        public int Id { get; set; }
        public SuiviPrerequis SuiviPrerequis { get; set; }
        public List<SuiviExercice> LesSuiviExercices { get; set; }
        public Niveau Niveau { get; set; }
        public EtatEnum Etat { get; set; }
        public DateTime DateValide { get; set; }
        public List<Note> LesNotes { get; set; }

        public EtatEnum EtatMaj()
        {
            bool premierValide = LesSuiviExercices[0].Valide ? true:false; ;
            bool valide = false;
            bool nonValide = false;
            foreach (var e in LesSuiviExercices)
            {
                if (e.Valide) valide = true;
                if (!e.Valide) nonValide = true;

                if (valide && nonValide) // s'il y a un Exercice validé et un autre non valide, le niveau est en cours
                {
                    Etat = EtatEnum.e2;
                    return Etat;
                }
            }
            if (premierValide) Etat = EtatEnum.e3; //s'il n'est pas en cours, il est vide ou Validé
            else Etat = EtatEnum.e1;
            return Etat;
        }
    }

    public class Niveau
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public string Intitule { get; set; }
        public List<SuiviNiveau> LesSuiviNiveaux { get; set; }

        public List<PrerequisNiveau> LesPrerequisNiveaux { get; set; }

        public Niveau() { }
        public Niveau(string intitule) 
        { 
            Intitule = intitule; 
        }
    }

    public class PrerequisNiveau
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public Niveau Niveau { get; set;}

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public Prerequis Prerequis { get; set; }
    }
}
