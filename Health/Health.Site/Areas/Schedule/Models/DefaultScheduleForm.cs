using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models;

namespace Health.Site.Areas.Schedule.Models
{
    public class DefaultScheduleFormModel : CoreViewModel
    {
        public DefaultSchedule DefaultSchedule { get; set; }
    }
}