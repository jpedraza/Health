using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Schedules.Models
{
    public class PersonalScheduleList : CoreViewModel
    {
        public IEnumerable<PersonalSchedule> PersonalSchedules { get; set; }
    }

    public class PersonalScheduleForm : CoreViewModel
    {
        public PersonalSchedule PersonalSchedule { get; set; }

        public string Message { get; set; }
    }
}