using System;
using Health.Core.Entities.Virtual;

namespace Health.Core.Entities.POCO.Abstract
{
    /// <summary>
    /// Базовый интерфейс расписаний.
    /// </summary>
    public interface ISchedule : IEntity, IKey
    {
        /// <summary>
        /// Параметр для которого составлено расписание.
        /// </summary>
        Parameter Parameter { get; set; }

        /// <summary>
        /// Начало промежутка времени когда должен быть заполнен параметр.
        /// </summary>
        TimeSpan TimeStart { get; set; }

        /// <summary>
        /// Конец промежутка времени когда должен быть заполнен параметр.
        /// </summary>
        TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// День в который должен быть запомнен праметр.
        /// </summary>
        Day Day { get; set; }

        /// <summary>
        /// Месяц в который должен быть заполнен параметр.
        /// </summary>
        Month Month { get; set; }

        /// <summary>
        /// Неделя в которую должен быть заполнен параметр.
        /// </summary>
        Week Week { get; set; }
    }
}
