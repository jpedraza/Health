using System;
using System.Collections.Generic;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Services
{
    /// <summary>
    /// Сервис контролирует отношения между лечащим доктором и его пациентами.
    /// </summary>
    public interface IAttendingDoctorService : ICoreService
    {
        /// <summary>
        /// Изменить лечащего доктора у пациента.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="patientId">Идентификатор пациента.</param>
        void SetLedDoctorForPatient(int doctorId, int patientId);

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
        /// Получить расписание для доктора 
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="date">Дата.</param>
        /// <returns>Расписание доктора.</returns>
        IEnumerable<Appointment> GetDoctorSchedule(int doctorId, DateTime date);

        /// <summary>
        /// Существуют ли свободные записи для доктора на заданную дату.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="date">Дата</param>
        /// <returns>Есть или нет свободные записи.</returns>
        bool IssetFreeAppointmentForDoctor(int doctorId, DateTime date);

        /// <summary>
        /// Получит ближайшее свободную дату для записи к доктору.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="startDate">Стартовая дата для поиска.</param>
        /// <returns>Расписание для доктора.</returns>
        DateTime GetDateOfNearAppointment(int doctorId, DateTime startDate);
    }
}
