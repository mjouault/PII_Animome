using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class SuiviUser
    {
        public int Id { get; set; }
        public Suivi Suivi { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
