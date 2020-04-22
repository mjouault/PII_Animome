using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    /// <summary>
    /// Classe jointure entre un Patient et un Utilisateur, membre de l'équipe thérapeutique qui le suit
    /// </summary>
    public class PatientUser
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
