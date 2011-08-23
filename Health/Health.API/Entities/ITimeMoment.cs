using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.API.Entities
{
    /// <summary>
    /// Момент времени в течении которого предоставляется возможность заполнить параметр.
    /// </summary>
    public interface ITimeMoment
    {
        /// <summary>
        /// Начало интервала времени в течении которого возможно заполнять параметр.
        /// </summary>
        TimeSpan TimeStart { get; set; }

        /// <summary>
        /// Конец интервала времени в течении которого возможно заполнять параметр.
        /// </summary>
        TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// День в который можно заполнять параметр.
        /// </summary>
        IDay Day { get; set; }

        /// <summary>
        /// Месяц в который можно заполянть параметр.
        /// </summary>
        IMonth Month { get; set; }
    }
}
