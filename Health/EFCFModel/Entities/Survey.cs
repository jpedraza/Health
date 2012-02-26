using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Surveys"), ScaffoldTable(true), DisplayName("Хирургическая операция")]
    public class Survey : IIdentity
    {
        public Survey()
        {
            Patients = new List<Patient>();
        }

        [Required, DisplayName("Имя")]
        public string Name { get; set; }

        [Required, DisplayName("Описание")]
        public string Description { get; set; }

        [NotDisplay, DisplayName("Пациенты")]
        public virtual ICollection<Patient> Patients { get; set; }

        #region IIdentity Members

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        #endregion
    }
}