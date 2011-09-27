namespace Health.Core.API.Services
{
    /// <summary>
    /// Сервис контролирует отношения между лечащим доктором и его пациентами.
    /// </summary>
    public interface IAttendingDoctorService : ICoreService
    {
        /// <summary>
        /// Добавить ведомого пациента для доктора.
        /// </summary>
        /// <param name="doctor_id">Идентификатор доктора.</param>
        /// <param name="patient_id">Идентификатор пациента.</param>
        void AddLedPatient(int doctor_id, int patient_id);

        /// <summary>
        /// Удалить ведомого пациента для доктора.
        /// </summary>
        /// <param name="doctor_id">Идентификатор доктора.</param>
        /// <param name="patient_id">Идентификатор пациента.</param>
        void DeleteLedPatient(int doctor_id, int patient_id);
    }
}
