using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Metadata
{
    public class MonthMetadata
    {
        [Required]
        [Range(0, 12)]
        [DisplayName("Месяц в году")]
        public int InYear { get; set; }
    }
}