using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;

namespace Animome.ViewModels
{
    public class CompetenceCreateViewModel
    {
        public SelectList Domaines { get; set; } //Permet à l'utilisateur de sélectionner un Domaine ds la liste
        public string IntituleDomaine { get; set; }
        public Competence Competence { get; set; }

       // public string [] IntituleSelectionnes { get; set; }
    }

}
