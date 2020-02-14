using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Domaine : ElmtSuivi
    {
        public List<DomaineUser> LesDomaineUsers { get; set; }
    }
}
