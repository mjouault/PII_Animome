using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    /// <summary>
    /// Classe jointure entre unUtilisateur et un Domaine d'expertise
    /// </summary>
    public class DomaineUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public Domaine Domaine { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
