using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IDoctorRepository : ICoreRepository<Doctor>
    {
        Doctor GetById(int doctor_id);

        bool DeleteById(int doctor_id);

        Doctor GetByIdIfNotLedPatient(int doctor_id, int patient_id);
    }
}
