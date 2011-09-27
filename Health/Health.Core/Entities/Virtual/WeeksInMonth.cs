using System.Collections.Generic;
using System.Linq;

namespace Health.Core.Entities.Virtual
{
    /// <summary>
    /// Недели в месяце.
    /// </summary>
    public static class WeeksInMonth
    {
        private static readonly Dictionary<int, Week> _weeks = new Dictionary<int, Week>
                                                                   {
                                                                       {1, new Week
                                                                               {
                                                                                   Parity = ParityOfWeek.Odd
                                                                               }},
                                                                       {2, new Week
                                                                               {
                                                                                   Parity = ParityOfWeek.Even
                                                                               }},
                                                                       {3, new Week
                                                                               {
                                                                                   Parity = ParityOfWeek.All
                                                                               }}
                                                                   };

        /// <summary>
        /// Нечетная неделя.
        /// </summary>
        public static readonly Week Odd = _weeks[1];

        /// <summary>
        /// Четная неделя.
        /// </summary>
        public static readonly Week Even = _weeks[2];

        /// <summary>
        /// Любая неделя.
        /// </summary>
        public static readonly Week All = _weeks[3];

        /// <summary>
        /// Получить неделю по ее номеру.
        /// </summary>
        /// <param name="index">Номер недели.</param>
        /// <returns>Неделя.</returns>
        public static Week Week(int index)
        {
            return (index <= _weeks.Count & index > 0) ? _weeks[index] : null;
        }
    }
}