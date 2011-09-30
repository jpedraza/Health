using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IPatientRepository : ICoreRepository<Patient>
    {
        Patient GetById(int patientId);

        Patient GetByIdIfNotLedDoctor(int patientId, int doctorId);

        bool DeleteById(int patientId);
    }
}
