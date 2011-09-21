using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;

namespace Health.Site.Models.Metadata
{
    public class DefaultScheduleMetadata
    {
        [DisplayName("Идентификатор")]
        public int Id { get; set; }

        [DisplayName("Период ввода параметра.")]
        public Period Period { get; set; }

        [DisplayName("Параметр.")]
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
}