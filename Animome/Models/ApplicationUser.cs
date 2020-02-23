using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Animome.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }

        public List<SuiviApplicationUser> LesSuiviApplicationUsers { get; set; }
        public List<SuiviExercice> LesSuiviExercices { get; set; }
    }
}
