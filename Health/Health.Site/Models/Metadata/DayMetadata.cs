using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class DayMetadata
    {
        [Required]
        [Range(0, 7)]
        [DisplayName("День недели.")]
        public int InWeek { get; set; }

        [Required]
        [Range(0, 31)]
        [DisplayName("День месяца.")]
        public int InMonth { get; set; }
    }
}