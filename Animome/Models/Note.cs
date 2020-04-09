using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Note
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public SuiviNiveau SuiviNiveau { get; set; }
        public DateTime Date { get; set; }
        public string Texte { get; set; }
    }
}
