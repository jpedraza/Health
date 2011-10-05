using System.Collections.Generic;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Доктор.
    /// </summary>
    public class Doctor : User
    {
        /// <summary>
        /// Специальность доктора.
        /// </summary>
        public Specialty Specialty { get; set; }

        /// <summary>
        /// Ведомые пациенты.
        /// </summary>
        public IList<Patient> Patients { get; set; }

        /// <summary>
        /// Расписание доктора.
        /// </summary>
        public WorkWeek WorkWeek { get; set; }
    }
}
