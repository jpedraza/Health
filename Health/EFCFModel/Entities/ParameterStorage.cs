using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Attributes;

namespace Model.Entities
{
    [DisplayName("Хранилище параметров"), Table("ParametersStorage")]
    public class ParameterStorage : IIdentity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Пациент")]
        [Required(ErrorMessage = "Необходимо указать пациента.")]
        public virtual Patient Patient { get; set; }

        [DisplayName("Параметр")]
        [Required(ErrorMessage = "Необходимо указать параметр.")]
        public virtual Parameter Parameter { get; set; }

        [DisplayName("Дата")]
        [Required(ErrorMessage = "Необходимо указать дату.")]
        public DateTime Date { get; set; }

        [DisplayName("Значение"), NotDisplay, ByteType("Parameter.ValueType")]
        public byte[] Value { get; set; }
    }
}
