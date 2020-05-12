using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;
using System.ComponentModel.DataAnnotations;

namespace Animome.ViewModels
{
    public class DomaineUserCreateViewModel
    {
        public SelectList Domaines { get; set; }

        public Domaine Domaine { get; set; }
    }
}
