using System;
namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Кандидат на регистрацию.
    /// </summary>
    public class Candidate : User
    {
        ///// <summary>
        ///// Номер полюса.
        ///// </summary>
        public string Policy { get; set; }

        ///// <summary>
        ///// Номер больничной карты.
        ///// </summary>
        public string Card { get; set; }

        /// <summary>
        /// Мать пациента
        /// </summary>
        public string Mother { get; set; }

        /// <summary>
        /// Дата начала наблюдения
        /// </summary>
        public DateTime StartDateOfObservation { get; set; }

        /// <summary>
        /// Контактный телефон 1
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// Контактный телефон 2
        /// </summary>
        public string Phone2 { get; set; }
    }
}