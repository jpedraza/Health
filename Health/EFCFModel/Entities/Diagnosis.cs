using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Diagnosis"), ScaffoldTable(true), DisplayName("Диагноз")]
    public class Diagnosis : IIdentity
    {
        public Diagnosis()
        {
            Patients = new List<Patient>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо указать имя.")]
        public string Name { get; set; }

        [DisplayName("Код")]
        [Required(ErrorMessage = "Необходимо указать код.")]
        public string Code { get; set; }

        [NotDisplay, DisplayName("Класс диагноза")]
        [Required(ErrorMessage = "Необходимо указать класс диагноза.")]
        public virtual DiagnosisClass DiagnosisClass { get; set; }

        [NotDisplay, DisplayName("Пациенты с таким диагнозом")]
        public virtual ICollection<Patient> Patients { get; set; }
    }
}