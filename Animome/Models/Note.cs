using System;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    /// <summary>
    /// Note de précision que peut laisser un utilisateur au moment de la validation d'un niveau
    /// </summary>
    public class Note
    {
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public SuiviNiveau SuiviNiveau { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Texte { get; set; }
    }
}
