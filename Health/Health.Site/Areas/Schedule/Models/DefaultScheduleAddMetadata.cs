using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Site.Models.Metadata;

namespace Health.Site.Areas.Schedule.Models
{
    public class DefaultScheduleAddMetadata : ParameterMetadata
    {
        [DisplayName("Идентификатор параметра.")]
        [Required(ErrorMessage = "Нужно указать номер параметра.")]
        public new int ParameterId { get; set; }
    }
}