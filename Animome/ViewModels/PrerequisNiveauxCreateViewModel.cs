using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;

namespace Animome.ViewModels
{
    public class PrerequisNiveauxCreateViewModel
    {
        public SelectList ListePrerequis { get; set; }
        public Prerequis Prerequis { get; set; }

        public SelectList ListeNiveaux { get; set; } //Permet à l'utilisateur de sélectionner un Niveau ds la liste
        public Niveau Niveau { get; set; }
    }
}
