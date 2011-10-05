using System;
using System.ComponentModel;
using Health.Core.Entities.Virtual;

namespace Health.Site.Models.Metadata
{
    public class WorkDayMetadata
    {
        [DisplayName("Рабочий день?")]
        public bool IsWeekEndDay { get; set; }

        [DisplayName("День недели.")]
        public Day Day { get; set; }

        [DisplayName("Начало рабочего дня.")]
        public TimeSpan TimeStart { get; set; }

        [DisplayName("Окончание рабочего дня.")]
        public TimeSpan TimeEnd { get; set; }

        [DisplayName("Начало обеда.")]
        public TimeSpan DinnerStart { get; set; }

        [DisplayName("Окончание обеда.")]
        public TimeSpan DinnerEnd { get; set; }

        [DisplayName("Начало приемного времени.")]
        public TimeSpan AttendingHoursStart { get; set; }

        [DisplayName("Окончание приемного времени.")]
        public TimeSpan AttendingHoursEnd { get; set; }
    }
}