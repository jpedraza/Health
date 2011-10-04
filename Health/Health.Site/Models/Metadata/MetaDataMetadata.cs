using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;

namespace Health.Site.Models.Metadata
{
    public class MetaDataMetadata
    {
        [DisplayName("Есть ли варианты ответа на параметр?")]
        public virtual bool Is_var { get; set; }

        [DisplayName("Возраст, в котором будет отслеживаться данный параметр")]
        public virtual object Age { get; set; }

        [DisplayName("Номер категории, к которому относится параметр")]
        public virtual Nullable<int> Id_cat { get; set; }

        [DisplayName("Обязателен ли параметр к заполнению?")]
        public virtual bool Obligation { get; set; }

        [DisplayName("Есть ли у данного параметра подпараметры?")]
        public virtual bool Is_childs { get; set; }

        [DisplayName("Номер родителя параметра.")]
        public virtual Nullable<int> Id_parent { get; set; }

        [DisplayName("Минимальное значение параметра")]
        public virtual object MinValue { get; set; }

        [DisplayName("Максимальное значение параметра")]
        public virtual object MaxValue { get; set; }

        [DisplayName("Варианы ответа на данный параметр")]
        public virtual Variant[] Variants { get; set; }

        [DisplayName("Список подпараметров")]
        public virtual IList<bool> Childs { get; set; }

        [DisplayName("Список родителей параметра")]
        public virtual IList<int> Parents { get; set; }
    }

    public class MetaDataFormMetadata : MetaDataMetadata
    {
        [Required(ErrorMessage="Необходимо указать возраст")]
        public override object Age { get; set; }
    }


}