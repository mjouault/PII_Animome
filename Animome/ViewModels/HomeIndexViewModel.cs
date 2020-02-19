using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animome.Models;


namespace Animome.ViewModels
{
    public class HomeIndexViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Patient Patient { get; set; }
    }
}
