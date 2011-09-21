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
}