using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Core.Entities.Virtual;

namespace Health.Site.Models.Metadata
{
    public class WeekMetadata
    {
        [Required]
        [DisplayName("Четность недели")]
        public ParityOfWeek Parity { get; set; }

        [Required]
        [Range(0, 6)]
        [DisplayName("Номер недели в месяце")]
        public int InMonth { get; set; }
    }
}