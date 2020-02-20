using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class CompetencePrerequis
    {
        public int Id { get; set; }
        public Competence Competence { get; set; }
        public Prerequis Prerequis { get; set; }
    }
}
