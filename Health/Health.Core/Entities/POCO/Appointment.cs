using System;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Прием у врача.
    /// </summary>
    public class Appointment : IEntity, IKey
    {
        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Тот кто идет к врачу.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// К какому врачу идем.
        /// </summary>
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Когда идем к врачу.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
