using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;

namespace Health.Site.Models.Metadata
{
    public class PersonalScheduleMetadata
    {
        [DisplayName("Идентификатор расписания")]
        public int Id { get; set; }

        [DisplayName("Пациент")]
        public Patient Patient { get; set; }

        [DisplayName("Параметр")]
        public Parameter Parameter { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Время начальное")]
        public TimeSpan TimeStart { get; set; }

        [DisplayName("Время окончания")]
        public TimeSpan TimeEnd { get; set; }

        [DisplayName("День")]
        public Day Day { get; set; }

        [DisplayName("Неделя")]
        public Week Week { get; set; }

        [DisplayName("Месяц")]
        public Month Month { get; set; }
    }
}