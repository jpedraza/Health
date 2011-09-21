using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models.Metadata;

namespace Health.Site.Areas.Schedules.Models
{
    public class DefaultScheduleAddMetadata : DefaultScheduleMetadata
    {
        public new int Id { get; set; }

        public new Period Period { get; set; }

        public new Parameter Parameter { get; set; }

        public new TimeSpan TimeStart { get; set; }

        public new TimeSpan TimeEnd { get; set; }

        public new Day Day { get; set; }

        public new Month Month { get; set; }

        public new Week Week { get; set; }
    }

    public  class PeriodAddMetadata : PeriodMetadata
    {
        [DisplayName("Ujlsssssssssssss")]
        [Required(ErrorMessage = "Необходимо указать число лет.")]
        [Range(0, 200, ErrorMessage = "Число лет должно быть от 0 до 200.")]
        public new int Years { get; set; }

        [Required(ErrorMessage = "Необходимо указать число месяцев.")]
        [Range(0, 12, ErrorMessage = "Число месяцев должно быть от 0 до 12.")]
        public new int Months { get; set; }

        [Required(ErrorMessage = "Необходимо указать число недель.")]
        [Range(0, 4, ErrorMessage = "Число недель должно быть от 0 до 4.")]
        public new int Weeks { get; set; }

        [Required(ErrorMessage = "Необходимо указать число дней.")]
        [Range(0, 31, ErrorMessage = "Число дней должно быть от 0 до 31.")]
        public new int Days { get; set; }

        [Required(ErrorMessage = "Необходимо указать число часов.")]
        [Range(0, 23, ErrorMessage = "Число часов должно быть от 0 до 23.")]
        public new int Hours { get; set; }

        [Required(ErrorMessage = "Необходимо указать число минут.")]
        [Range(0, 59, ErrorMessage = "Число минут должно быть от 0 до 59.")]
        public new int Minutes { get; set; }
    }
}