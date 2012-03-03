using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("FunctionalClasses"), ScaffoldTable(true), DisplayName("Функциональный класс")]
    public class FunctionalClass : IIdentity
    {
        public FunctionalClass()
        {
            Patients = new List<Patient>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Код")]
        [Required(ErrorMessage = "Необходимо указать код класса.")]
        public string Code { get; set; }

        [DisplayName("Описание"), EditMode(EditMode.Multiline)]
        [Required(ErrorMessage = "Необходимо добавить описание класса.")]
        public string Description { get; set; }

        [DisplayName("Пациенты"), NotDisplay]
        public virtual ICollection<Patient> Patients { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}