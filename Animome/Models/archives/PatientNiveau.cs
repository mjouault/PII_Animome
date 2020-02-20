using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class PatientNiveau
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Niveau Niveau { get; set; }

        public int NbExercices { get; set; }
        public int NbExercicesAccomplis { get; set; }
    }
}

