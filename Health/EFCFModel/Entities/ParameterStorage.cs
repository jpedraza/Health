using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [ScaffoldTable(true), DisplayName("Хранилище параметров"), Table("ParametersStorage")]
    public class ParameterStorage : IIdentity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [Required, DisplayName("Пациент")]
        public virtual Patient Patient { get; set; }

        [Required, DisplayName("Параметр")]
        public virtual Parameter Parameter { get; set; }

        [Required, DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Значение"), NotDisplay]
        public byte[] Value { get; set; }
    }
}
