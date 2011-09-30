using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models.Providers;

namespace Health.Site.Models.Metadata
{
    public class DefaultScheduleMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Период ввода параметра.")]
        public virtual Period Period { get; set; }

        [DisplayName("Параметр.")]
        //[ClassMetadata(typeof(IfSubParameterMetadata))]
        public virtual Parameter Parameter { get; set; }

        [DisplayName("Время начала ввода параметра.")]
        public virtual TimeSpan TimeStart { get; set; }

        [DisplayName("Время окончания ввода параметра.")]
        public virtual TimeSpan TimeEnd { get; set; }

        [DisplayName("День в который возможен ввод параметра.")]
        public virtual Day Day { get; set; }

        [DisplayName("Месяц в который возможен ввод параметра.")]
        public virtual Month Month { get; set; }

        [DisplayName("Неделя в которую возможен ввод параметра.")]
        public virtual Week Week { get; set; }
    }

    public class DefaultScheduleFormMetadata : DefaultScheduleMetadata
    {
        [Required(ErrorMessage = "Укажите идентификатор расписания.")]
        public override int Id { get; set; }

        [ClassMetadata(typeof(PeriodEditMetadata))]
        public override Period Period { get; set; }

        [Required(ErrorMessage = "Укажите время начала ввода параметра")]
        public override TimeSpan TimeStart { get; set; }

        [Required(ErrorMessage = "Укажите время окончания ввода параметра")]
        public override TimeSpan TimeEnd { get; set; }
    }
}