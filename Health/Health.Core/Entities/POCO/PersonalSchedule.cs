using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    public class PersonalSchedule : Schedule
    {
        public Patient Patient { get; set; }

        public Diagnosis Diagnosis { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
    }
}
