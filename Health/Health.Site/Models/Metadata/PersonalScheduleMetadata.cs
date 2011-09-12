using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Metadata
{
    public class PersonalScheduleMetadata
    {
        [DisplayName("Идентификатор расписания")]
        public int Id { get; set; }

        [DisplayName("Диагноз")]
        public Diagnosis Diagnosis { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Время начальное")]
        public TimeSpan TimeStart { get; set; }

        [DisplayName("Время окончания")]
        public TimeSpan TimeEnd { get; set; }
    }
}