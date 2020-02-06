using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Competence: ElmtSuivi
    {
        public List <Prerequis> LesPrerequis { get; set;}
        public Competence(int id, string intitule) : base(id, intitule) {
            LesPrerequis = new List<Prerequis>();
        }
    }
}
