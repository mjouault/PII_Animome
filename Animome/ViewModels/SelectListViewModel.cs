using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;

namespace Animome.ViewModels
{
    public class SelectListViewModel
    {
        public SelectList Domaines { get; set; } //Permet à l'utilisateur de sélectionner un genre ds la liste
        public string IntituleDomaine { get; set; }

        public Domaine Domaine { get; set; }
    }

}
