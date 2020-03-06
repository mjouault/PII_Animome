using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animome.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Animome.ViewModels
{
    public class PrerequisCreateViewModel
    {
        public SelectList Competences { get; set; } //Permet à l'utilisateur de sélectionner une compétence ds la liste
        public string IntituleCompetence { get; set; }
        public Prerequis Prerequis { get; set; }
    }
}
