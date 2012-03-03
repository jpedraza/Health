using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [ScaffoldTable(true), DisplayName("Доктор")]
    public class Doctor : User
    {
        public Doctor()
        {
            Appointments = new List<Appointment>();
        }

        [NotDisplay, DisplayName("Приемы у врача")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [NotDisplay, DisplayName("Пациенты")]
        public virtual ICollection<Patient> Patients { get; set; }

        [DisplayName("Специальность"), NotDisplay]
        [Required(ErrorMessage = "Необходимо указать специальность")]
        public Specialty Specialty { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}