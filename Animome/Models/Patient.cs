﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animome.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int Identifiant { get; set; } 

        //public List<PatientNiveau> LesPatientsNiveaux;

        public List<Suivi> lesSuivis;
    }
}
