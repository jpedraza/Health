using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attributes;

namespace Model.Entities
{
    [Table("FunctionalAbnormalities"), ScaffoldTable(true), DisplayName("Функциональное нарушение")]
    public class FunctionalAbnormality : IIdentity
    {
        public FunctionalAbnormality()
        {
            ChildFunctionalAbnormalities = new List<FunctionalAbnormality>();
            Patients = new List<Patient>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо указать имя.")]
        public string Name { get; set; }

        [NotDisplay, DisplayName("Дочерние функциональные нарушения")]
        public virtual ICollection<FunctionalAbnormality> ChildFunctionalAbnormalities { get; set; }

        [NotDisplay, DisplayName("Родительское функциональное нарушение")]
        public virtual FunctionalAbnormality ParentFunctionalAbnormality { get; set; }

        [NotDisplay, DisplayName("Пациенты")]
        public virtual ICollection<Patient> Patients { get; set; }
    }
}