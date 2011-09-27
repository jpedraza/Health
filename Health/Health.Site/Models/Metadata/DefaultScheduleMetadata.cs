using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Attributes;
using Health.Site.Models.Providers;

namespace Health.Site.Models.Metadata
{
    public class DefaultScheduleMetadata
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Период ввода параметра.")]
        public Period Period { get; set; }

        [DisplayName("Параметр.")]
        [ClassMetadata(typeof(IfSubParameterMetadata))]
        public Parameter Parameter { get; set; }

        [DisplayName("Время начала ввода параметра.")]
        public TimeSpan TimeStart { get; set; }

        [DisplayName("Время окончания ввода параметра.")]
        public TimeSpan TimeEnd { get; set; }

        [DisplayName("День в который возможен ввод параметра.")]
        public Day Day { get; set; }

        [DisplayName("Месяц в который возможен ввод параметра.")]
        public Month Month { get; set; }

        [DisplayName("Неделя в которую возможен ввод параметра.")]
        public Week Week { get; set; }
    }

    public class DefaultScheduleFormMetadata : DefaultScheduleMetadata
    {
        [Required(ErrorMessage = "Укажите идентификатор расписания.")]
        public new int Id { get; set; }

        [Required(ErrorMessage = "Укажите время начала ввода параметра")]
        public new TimeSpan TimeStart { get; set; }

        [Required(ErrorMessage = "Укажите время окончания ввода параметра")]
        public new TimeSpan TimeEnd { get; set; }
    }
}