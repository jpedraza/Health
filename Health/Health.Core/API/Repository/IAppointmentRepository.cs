using System;
using System.Collections.Generic;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Репозиторий журнала приема у врача.
    /// </summary>
    public interface IAppointmentRepository : ICoreRepository<Appointment>
    {
        /// <summary>
        /// Получить все записи на прием для доктора на сегодня.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <returns>Записи на прием.</returns>
        IEnumerable<Appointment> GetNowDayAppointmentsForDoctor(int doctorId);

        /// <summary>
        /// Получить все записи на прием для пациента на сегодня.
        /// </summary>
        /// <param name="patientId">Идентификатор пациента.</param>
        /// <returns>Записи на прием.</returns>
        IEnumerable<Appointment> GetNowDayAppointmentsForPatient(int patientId);

        /// <summary>
        /// Получить все записи на прием для доктора согласно дате.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="date">Дата приема.</param>
        /// <returns>Записи на прием.</returns>
        IEnumerable<Appointment> GetAppointmentForDoctorByDate(int doctorId, DateTime date);

        /// <summary>
        /// Получить число записей к врачу согласно дате.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="date">Дата.</param>
        /// <returns>Чило записей.</returns>
        int CountAppointment(int doctorId, DateTime date);
    }
}
