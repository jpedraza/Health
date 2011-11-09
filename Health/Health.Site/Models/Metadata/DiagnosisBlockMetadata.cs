using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Metadata
{
    public class DiagnosisBlockMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Имя блока диагнозов")]
        public virtual string Name { get; set; }

        [DisplayName("Код блока диагнозов")]
        public virtual string Code { get; set; }

        [DisplayName("Диагнозы, входящие в этот блок")]
        public virtual IList<Diagnosis> Diagnosises { get; set; }
    }

    public class DiagnosisBlockFormMetadata : DiagnosisBlockMetadata
    {
        [Required]
        public override string Name { get; set; }

        [Required]
        public override string Code { get; set; }
    }
}