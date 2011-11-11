using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using System.Collections;
using System.Collections.Generic;

namespace Health.Site.Models.Metadata
{
    public class SurgeryMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Название операции/лечения")]
        public virtual string Name { get; set; }

        [DisplayName("Описание операци/лечения")]
        public virtual string Description { get; set; }
    }

    public class SurgeryFormMetada : SurgeryMetadata
    {
        [Required(ErrorMessage = "Введите описание операции/лечения")]
        public override string Description { get; set; }

        [Required(ErrorMessage = "Введите название операции/лечения")]
        public override string Name { get; set; }        
    }
}