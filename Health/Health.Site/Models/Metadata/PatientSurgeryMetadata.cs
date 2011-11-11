using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using System.Collections;
using System.Collections.Generic;
namespace Health.Site.Models.Metadata
{
    public class PatientSurgeryMetadata : SurgeryMetadata
    {
        [DisplayName("Дата выполнения операции/лечения")]
        public virtual DateTime DateSurgery { get; set; }

        [DisplayName("Статус выполнени операции/лечения")]
        public virtual bool Status { get; set; }
    }

    public class PatientSurgeryFormMetadata : PatientSurgeryMetadata
    {
        [Required(ErrorMessage = "Введите дату выполнения операции/лечения")]
        public override DateTime DateSurgery { get; set; }

        [Required(ErrorMessage = "Введите описание операции/лечения")]
        public override string Description { get; set; }

        [Required(ErrorMessage = "Введите название операции/лечения")]
        public override string Name { get; set; }
    }
}