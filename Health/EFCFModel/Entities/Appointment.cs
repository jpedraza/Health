using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attributes;

namespace Model.Entities
{
    [Table("Appointments"), ScaffoldTable(true), DisplayName("Прием у врача")]
    public class Appointment : IIdentity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Дата приема")]
        [Required(ErrorMessage = "Необходимо указать дату приема.")]
        public DateTime Date { get; set; }

        [DisplayName("Описание"), NotDisplay, EditMode(EditMode.Multiline)]
        public string Description { get; set; }

        [DisplayName("Доктор")]
        [Required(ErrorMessage = "Необходимо указать доктора.")]
        public virtual Doctor Doctor { get; set; }

        [DisplayName("Пациент")]
        [Required(ErrorMessage = "Необходимо указать пациента.")]
        public virtual Patient Patient { get; set; }

        public override string ToString()
        {
            return string.Format("{0} к {1} на {2}", Patient, Doctor, Date);
        }
    }
}