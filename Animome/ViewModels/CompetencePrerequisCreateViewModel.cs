using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;
using Animome.ViewModels;
namespace Animome.ViewModels
{
    public class CompetencePrerequisCreateViewModel
    {
        public SelectList ListeCompetences { get; set; }
        public Competence Competence { get; set; }

        public SelectList ListePrerequis { get; set; } //Permet à l'utilisateur de sélectionner un Domaine ds la liste
        public Prerequis Prerequis { get; set; }

    }
}
