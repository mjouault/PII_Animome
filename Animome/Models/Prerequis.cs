using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Prerequis : ElmtSuivi
    {
        public List<Niveau> LesNivx { get; set; } //mettre en ICollection
        public Competence LaCompetence {get;set;}
        public Prerequis(int id, string intitule) : base(id, intitule) {
            LesNivx = new List<Niveau>();
        }
    }
}
