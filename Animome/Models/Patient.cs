using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int Numero { get; set; } 

        //public List<PatientNiveau> LesPatientsNiveaux;

        public List<Suivi> LesSuivis { get; set; }
        public List<PatientUser> LesSuiveurs { get; set; }
    }
}
