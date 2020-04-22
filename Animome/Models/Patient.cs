using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Animome.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ce champ ne peut être vide")]
        public int Numero { get; set; } //Pour des raisons de protection de données, le patient n'est défini que par un numéro

        public List<Suivi> LesSuivis { get; set; }
        public List<PatientUser> LesSuiveurs { get; set; }
    }
}
