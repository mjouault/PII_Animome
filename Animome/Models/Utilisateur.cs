using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        private string Login { get; set; }
        private string Mdp { get; set; }
        public int email { get; set; }

        public List<Domaine> lesDomaines { get; set; }
    }
}
