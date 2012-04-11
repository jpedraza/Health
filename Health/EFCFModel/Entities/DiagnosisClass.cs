using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attributes;

namespace Model.Entities
{
    [Table("DiagnosisClasses"), ScaffoldTable(true), DisplayName("Класс диагноза")]
    public class DiagnosisClass : IIdentity
    {
        public DiagnosisClass()
        {
            Diagnosis = new List<Diagnosis>();
            ChildDiagnosisClasses = new List<DiagnosisClass>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо указать имя класса.")]
        public string Name { get; set; }

        [DisplayName("Код")]
        [Required(ErrorMessage = "Необходимо указать код класса.")]
        public string Code { get; set; }

        [DisplayName("Диагнозы"), NotDisplay]
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }

        [DisplayName("Дочерние классы диагнозов"), NotDisplay]
        public virtual ICollection<DiagnosisClass> ChildDiagnosisClasses { get; set; }

        [DisplayName("Родительский класс диагноза"), NotDisplay]
        public virtual DiagnosisClass ParentDiagnosisClass { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, Code);
        }
    }
}