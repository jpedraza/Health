using System;

namespace Health.Core.Entities.Virtual
{
    /// <summary>
    /// Интервал времени.
    /// </summary>
    public class TimeInterval
    {
        /// <summary>
        /// Начало промежутка времени когда должен быть заполнен параметр.
        /// </summary>
        public TimeSpan TimeStart { get; set; }

        /// <summary>
        /// Конец промежутка времени когда должен быть заполнен параметр.
        /// </summary>
        public TimeSpan TimeEnd { get; set; }
    }
}