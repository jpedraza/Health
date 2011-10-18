using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IDoctorRepository : ICoreRepository<Doctor>
    {
        Doctor GetById(int doctorId);

        bool DeleteById(int doctorId);

        Doctor GetByIdIfNotLedPatient(int doctorId, int patientId);
    }
}
