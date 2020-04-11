using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Animome.ViewModels
{
    public class PatientUserCreateViewModel
    {
        public SelectList ListeUsers { get; set; }
        public string NomUser { get; set; }

    }
}
