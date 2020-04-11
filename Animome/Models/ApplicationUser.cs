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

        public List<SuiviUser> LesSuiviApplicationUsers { get; set; }
        public List<SuiviExercice> LesSuiviExercices { get; set; }

        public List<DomaineUser> LesDomaines { get; set; }

        public List<PatientUser> LesPatients { get; set; }

        public string Role { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
    }
}
