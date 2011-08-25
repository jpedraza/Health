using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Site.Areas.Schedule.Models.Forms;
using Health.Site.Models;

namespace Health.Site.Areas.Schedule.Models
{
    public class ScheduleViewModel : CoreViewModel
    {
        public DefaultScheduleForm DefaultScheduleForm { get; set; }
    }
}