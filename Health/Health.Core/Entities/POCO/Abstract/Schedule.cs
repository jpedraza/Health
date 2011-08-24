using System;
using Health.Core.Entities.Virtual;

namespace Health.Core.Entities.POCO.Abstract
{
    public abstract class Schedule
    {
        public Parameter Parameter { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }

        public Day Day { get; set; }

        public Month Month { get; set; }

        public Week Week { get; set; }
    }
}
