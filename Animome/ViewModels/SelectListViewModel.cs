using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Animome.ViewModels
{
    public class SelectListViewModel
    {
        public SelectList Domaines { get; set; } //Permet à l'tuilisateur de sélectionner un genre ds la liste
        public string IntituleDomaine { get; set; }
    }

}
