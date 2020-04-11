using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Animome.Models;
using Animome.ViewModels;

namespace Animome.ViewModels
{
    public class DomaineUserCreateViewModel
    {
        public SelectList Domaines { get; set; }
        public Domaine Domaine { get; set; }
    }
}
