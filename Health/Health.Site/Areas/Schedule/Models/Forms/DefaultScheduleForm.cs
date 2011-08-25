using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;

namespace Health.Site.Areas.Schedule.Models.Forms
{
    public class DefaultScheduleForm
    {
        public IEnumerable<DefaultSchedule> DefaultSchedules { get; set; }
    }
}