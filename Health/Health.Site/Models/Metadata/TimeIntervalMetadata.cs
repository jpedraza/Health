using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class TimeIntervalMetadata
    {
        [DisplayName("Время начала ввода параметра.")]
        [Required]
        public TimeSpan TimeStart { get; set; }

        [DisplayName("Время окончания ввода параметра.")]
        [Required]
        public TimeSpan TimeEnd { get; set; }
    }
}