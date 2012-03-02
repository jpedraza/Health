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
        public virtual Patient Patient { get; set; }

        [DisplayName("Операция")]
        public virtual Survey Survey { get; set; }

        [DisplayName("Дата операции")]
        public DateTime Date { get; set; }

        [DisplayName("Описание"), NotDisplay, EditMode(EditMode.Multiline)]
        public string Description { get; set; }
    }
}
