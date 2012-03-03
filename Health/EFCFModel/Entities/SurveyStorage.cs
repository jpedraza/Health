using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("SurveysStorage"), ScaffoldTable(true), DisplayName("Хранилище операций")]
    public class SurveyStorage : IIdentity
    {
        [Hide, Key, DatabaseGenerated((DatabaseGeneratedOption.Identity))]
        public int Id { get; set; }

        [DisplayName("Пациент")]
        [Required(ErrorMessage = "Необходимо выбрать пациента.")]
        public virtual Patient Patient { get; set; }

        [DisplayName("Операция")]
        [Required(ErrorMessage = "Необходимо выбрать операцю.")]
        public virtual Survey Survey { get; set; }

        [DisplayName("Дата операции")]
        [Required(ErrorMessage = "Необходимо указать дату операции.")]
        public DateTime Date { get; set; }

        [DisplayName("Описание"), NotDisplay, EditMode(EditMode.Multiline)]
        public string Description { get; set; }
    }
}
