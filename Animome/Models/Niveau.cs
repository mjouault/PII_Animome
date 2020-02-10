using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Niveau : ElmtSuivi
    {
        public Niveau(int id, string intitule, bool complete) : base(id, intitule) { complete = false; }
    }
}
