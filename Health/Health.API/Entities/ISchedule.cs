using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.API.Entities
{
    public interface ISchedule
    {
        IPatient Patient { get; set; }

        IParameter Parameter { get; set; }

        DateTime DateStart { get; set; }

        DateTime DateEnd { get; set; }

        ITimeMoment TimeMoment { get; set; }
    }
}
