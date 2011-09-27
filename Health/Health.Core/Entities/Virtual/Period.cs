using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Core.Entities.Virtual
{
    /// <summary>
    /// Период времени.
    /// </summary>
    [Serializable]
    public class Period
    {
        /// <summary>
        /// Число лет.
        /// </summary>
        public int Years { get; set; }

        /// <summary>
        /// Число месяцев.
        /// </summary>
        public int Months { get; set; }

        /// <summary>
        /// Число недель.
        /// </summary>
        public int Weeks { get; set; }

        /// <summary>
        /// Число дней.
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// Число часов.
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Число минут.
        /// </summary>
        public int Minutes { get; set; }
    }
}
