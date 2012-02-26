using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Appointments"), ScaffoldTable(true), DisplayName("Прием у врача")]
    public class Appointment : IIdentity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Дата приема")]
        public DateTime Date { get; set; }

        [DisplayName("Доктор")]
        public virtual Doctor Doctor { get; set; }

        [DisplayName("Пациент")]
        public virtual Patient Patient { get; set; }

        public override string ToString()
        {
            return string.Format("{0} к {1} на {2}", Patient, Doctor, Date);
        }
    }
}