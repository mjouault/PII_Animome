using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Commentaire
    {
        public int Id { get; set; }
        public SuiviApplicationUser SuiviApplicationUser { get; set; }
        public string Texte { get; set; }
    }
}
