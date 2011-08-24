using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO.Abstract;
using Health.Core.Entities.Virtual;

namespace Health.Core.Entities.POCO
{
    public class DefaultSchedule : Schedule
    {
        public DateTime Period { get; set; }
    }
}
