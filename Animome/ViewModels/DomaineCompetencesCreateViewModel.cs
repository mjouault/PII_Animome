using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;
using Animome.ViewModels;

namespace Animome.ViewModels
{
    public class DomaineCompetencesCreateViewModel
    {
        public SelectList Domaines { get; set; } //Permet à l'utilisateur de sélectionner un Domaine ds la liste
        public Domaine Domaine { get; set; }

        public SelectList Competences { get; set; }
        public Competence Competence { get; set; }

        // public string [] IntituleSelectionnes { get; set; }
    }

}
