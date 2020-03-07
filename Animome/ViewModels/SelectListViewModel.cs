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
        public List<string> IntituleSelectionnes { get; set; }

        public List<Domaine> DomainesSelectionnes { get; set; }

        public SelectList Competences { get; set; }
        public string IntituleCompetence { get; set; }

        public SelectList Prerequis { get; set; }
        public string IntitulePrerequis { get; set; }

        public SelectList Niveaux { get; set; }
        public string IntituleNiveau { get; set; }


        public DomaineCompetence  DomaineCompetence {get;set;}
        public CompetencePrerequisCreateViewModel CompetencePrerequis { get; set; }
        public PrerequisNiveau PrerequisNiveau { get; set; }

    }

}
