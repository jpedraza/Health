using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class PeriodMetadata
    {
        [DisplayName("Годы")]
        public int Years { get; set; }

        [DisplayName("Месяцы")]
        public int Months { get; set; }

        [DisplayName("Недели")]
        public int Weeks { get; set; }

        [DisplayName("Дни")]
        public int Days { get; set; }

        [DisplayName("Часы")]
        public int Hours { get; set; }

        [DisplayName("Минуты")]
        public int Minutes { get; set; }
    }

    public class PeriodEditMetadata
    {
        [Required(ErrorMessage = "Необходимо указать число лет.")]
        [Range(0, 200, ErrorMessage = "Число лет должно быть от 0 до 200.")]
        public int Years { get; set; }

        [Required(ErrorMessage = "Необходимо указать число месяцев.")]
        [Range(0, 12, ErrorMessage = "Число месяцев должно быть от 0 до 12.")]
        public int Months { get; set; }

        [Required(ErrorMessage = "Необходимо указать число недель.")]
        [Range(0, 4, ErrorMessage = "Число недель должно быть от 0 до 4.")]
        public int Weeks { get; set; }

        [Required(ErrorMessage = "Необходимо указать число дней.")]
        [Range(0, 31, ErrorMessage = "Число дней должно быть от 0 до 31.")]
        public int Days { get; set; }

        [Required(ErrorMessage = "Необходимо указать число часов.")]
        [Range(0, 23, ErrorMessage = "Число часов должно быть от 0 до 23.")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Необходимо указать число минут.")]
        [Range(0, 59, ErrorMessage = "Число минут должно быть от 0 до 59.")]
        public int Minutes { get; set; }
    }
}