using System;

namespace Health.Core.Entities.Virtual
{
    /// <summary>
    /// Рабочий день.
    /// </summary>
    public class WorkDay
    {
        /// <summary>
        /// Это выходной день?
        /// </summary>
        public bool IsWeekEndDay { get; set; }

        /// <summary>
        /// День недели.
        /// </summary>
        public Day Day { get; set; }

        /// <summary>
        /// Начало рабочего дня.
        /// </summary>
        public TimeSpan TimeStart { get; set; }

        /// <summary>
        /// Окончание рабочего дня.
        /// </summary>
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// Начало обеда.
        /// </summary>
        public TimeSpan DinnerStart { get; set; }

        /// <summary>
        /// Окончание обеда.
        /// </summary>
        public TimeSpan DinnerEnd { get; set; }

        /// <summary>
        /// Начало приемного времени.
        /// </summary>
        public TimeSpan AttendingHoursStart { get; set; }

        /// <summary>
        /// Окончание приемного времени.
        /// </summary>
        public TimeSpan AttendingHoursEnd { get; set; }

        /// <summary>
        /// Длительность приема одного пациента.
        /// </summary>
        public int AttendingMinutes { get; set; }

        /// <summary>
        /// Число приемов у доктора в день.
        /// </summary>
        public int CountAppointment { get { return (int) (AttendingHoursStart - AttendingHoursEnd).TotalMinutes/AttendingMinutes; } }
    }
}
