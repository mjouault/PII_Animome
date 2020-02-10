using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Animome.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Genre { get; set; }
    }
}
