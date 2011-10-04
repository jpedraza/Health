using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Metadata
{
    public class VariantMetadata
    {
        [DisplayName("Значение варианта")]
        public virtual string Value { get; set; }

        [DisplayName("Балл, соответствующий данному варианту")]
        public virtual Nullable<double> Ball { get; set; }
    }

    public class VariantFormMetaData : VariantMetadata
    {
        [Required(ErrorMessage = "Введите значение варианта")]
        public override string Value { get; set; }

        [Required(ErrorMessage = "Введите балл")]
        public override Nullable<double> Ball { get; set; }
    }
}