using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;

namespace Health.Site.Models.Metadata
{
    public class ParameterMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Имя параметра")]
        public virtual string Name { get; set; }

        [DisplayName("Значение параметра")]
        public virtual object Value { get; set; }

        [DisplayName("Значение параметра по-умолчанию")]
        public virtual object DefaultValue { get; set; }

        [DisplayName("Мета-данные, описывающие параметр здоровья человека")]
        public virtual MetaData MetaData { get; set; }
    }

    public class IfSubParameterMetadata : ParameterMetadata
    {
        [Required(ErrorMessage = "Укажите параметр.")]
        public override int Id { get; set; }
    }

    public class ParameterFormMetadata : ParameterMetadata
    {
        [Required(ErrorMessage="Укажите имя параметра")]
        public override string Name { get; set; }

        [Required(ErrorMessage="Укажите значение")]
        public override object Value { get; set; }

        [Required(ErrorMessage = "Укажите значение параметра")]
        public override object DefaultValue { get; set; }

        [ClassMetadata(typeof(MetaDataFormMetadata))]
        public override MetaData MetaData { get; set; }
    }
}