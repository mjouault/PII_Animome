using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    /// <summary>
    /// Classe Utilisateur 
    /// </summary>
    /// 

    //ApplicationUser est un nom communément donné à une classe utilisateur héritant de Identity
    public class ApplicationUser : IdentityUser //Hérite de l'API de gestion des utilisateurs Identity intégrée à ASP.Net 
    {
        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Nom { get; set; }

        public List<SuiviExercice> LesSuiviExercices { get; set; } // Liste du plus petit élément qui peut être validé dans un suivi

        public List<DomaineUser> LesDomaines { get; set; } //Domaines d'expertises de l'utilisateur

        public List<PatientUser> LesPatients { get; set; }

        public string Role { get; set; } //2 rôles possibles : administrateur ou Utilisateur
    }
}
