using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Metadata
{
    public class DiagnosisClassMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Имя класса диагнозов")]
        public virtual string Name { get; set; }

        [DisplayName("Код (номер) класса диагнозов")]
        public virtual string Code { get; set; }

        [DisplayName("Блоки диагнозов, входящие в данный класс диагнозов")]
        public virtual IList<DiagnosisBlock> DiagnosisBlocks { get; set; }
    }

    public class DiagnosisClassFormMetadata : DiagnosisClassMetadata
    {
        [Required]
        public override string Name { get; set; }

        [Required]
        public override string Code { get; set; }
    }
}