using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO.Abstract;
using Health.Core.Entities.Virtual;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Персональное расписание.
    /// </summary>
    public class PersonalSchedule : ISchedule
    {
        /// <summary>
        /// Для какого пациента составлено.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// Для какого диагноза.
        /// </summary>
        public Diagnosis Diagnosis { get; set; }

        /// <summary>
        /// Дата начала замеров.
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Дата завершения замеров.
        /// </summary>
        public DateTime DateEnd { get; set; }

        #region Implementation of ISchedule

        /// <summary>
        /// Идентификатор расписания.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Параметр для которого составлено расписание.
        /// </summary>
        public Parameter Parameter { get; set; }

        /// <summary>
        /// Начало промежутка времени когда должен быть заполнен параметр.
        /// </summary>
        public TimeSpan TimeStart { get; set; }

        /// <summary>
        /// Конец промежутка времени когда должен быть заполнен параметр.
        /// </summary>
        public TimeSpan TimeEnd { get; set; }

        /// <summary>
        /// День в который должен быть запомнен праметр.
        /// </summary>
        public Day Day { get; set; }

        /// <summary>
        /// Месяц в который должен быть заполнен параметр.
        /// </summary>
        public Month Month { get; set; }

        /// <summary>
        /// Неделя в которую должен быть заполнен параметр.
        /// </summary>
        public Week Week { get; set; }

        #endregion
    }
}
