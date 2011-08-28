using System.Collections.Generic;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Schedule.Models
{
    public class DefaultScheduleList : CoreViewModel
    {
        public IEnumerable<DefaultSchedule> DefaultSchedules { get; set; }
    }
}