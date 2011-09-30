using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Metadata
{
    public class ParameterMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Имя параметра")]
        public virtual int Name { get; set; }
    }

    public class IfSubParameterMetadata : ParameterMetadata
    {
        [Required(ErrorMessage = "Укажите параметр.")]
        public override int Id { get; set; }
    }
}