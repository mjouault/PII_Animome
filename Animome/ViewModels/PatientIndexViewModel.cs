using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animome.Models;

namespace Animome.ViewModels
{
    public class PatientIndexViewModel
    {
        public List<Patient> Patients { get; set; }
        public string RecherchePatient { get; set; }

        public List<SuiviUser> LesSuiveurs {get;set ;}
    }
}
