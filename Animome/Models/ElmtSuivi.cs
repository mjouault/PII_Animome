using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public abstract class ElmtSuivi
    {
        public int Id { get; set; }
        public string Intitule { get; set; }

        public ElmtSuivi(int id, string intitule)
        {
            Id = id;
            Intitule = intitule;
        }
    }
}
