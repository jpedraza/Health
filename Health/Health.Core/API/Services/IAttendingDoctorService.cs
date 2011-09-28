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
        /// <param name="doctor_id">Идентификатор доктора.</param>
        /// <param name="patient_id">Идентификатор пациента.</param>
        void SetLedDoctorForPatient(int doctor_id, int patient_id);
    }
}
