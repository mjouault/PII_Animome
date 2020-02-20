using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class PatientCompetence
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Competence Competence { get; set; }
    }
}
