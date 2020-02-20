using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class NiveauPrerequis
    {
        public int Id { get; set; }
        public Niveau Niveau { get; set; }
        public Prerequis Prerequis { get; set; }
    }
}
