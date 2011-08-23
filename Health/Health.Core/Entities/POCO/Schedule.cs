using System;
using Health.Core.Entities.Virtual;

namespace Health.Core.Entities.POCO
{
    public class Schedule : Entity
    {
        public Patient Patient { get; set; }
        public Parameter Parameter { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public Day WeekDay { get; set; }
        public Month Month { get; set; }
        public Week Week { get; set; }
        public int MonthDay { get; set; }
    }
}