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

        public List<DomaineUser> LesDomaineUsers { get; set; }

        //public IdentityRole Role = new IdentityRole { Name = "enAttente" };
    }
}
