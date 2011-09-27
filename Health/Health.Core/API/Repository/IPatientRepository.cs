using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IPatientRepository : ICoreRepository<Patient>
    {
        Patient GetById(int patient_id);

        bool DeleteById(int patient_id);
    }
}
