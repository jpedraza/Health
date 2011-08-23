using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.API.Entities.Virtual
{
    public class TimeMoment : ITimeMoment
    {
        #region Implementation of ITimeMoment

        /// <summary>
        /// Начало интервала времени в течении которого возможно заполнять параметр.
        /// </summary>
        public TimeSpan TimeStart { get; set; }

        /// <summary>
        /// Конец интервала времени в течении которого возможно заполнять параметр.
        /// </summary>
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// День в который можно заполнять параметр.
        /// </summary>
        public IDay Day { get; set; }

        /// <summary>
        /// Месяц в который можно заполянть параметр.
        /// </summary>
        public IMonth Month { get; set; }

        #endregion
    }
}
