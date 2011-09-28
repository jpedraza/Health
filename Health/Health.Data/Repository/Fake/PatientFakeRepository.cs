using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class PatientFakeRepository : CoreFakeRepository<Patient>, IPatientRepository
    {
        public PatientFakeRepository(IDIKernel di_kernel) : base(di_kernel)
        {
            
        }

        public override bool Update(Patient entity)
        {
            entity.Doctor = DIKernel.Get<IDoctorRepository>().GetById(entity.Doctor.Id);
            return base.Update(entity);
        }

        public override bool Delete(Patient entity)
        {
            entity.Doctor.Patients.ToList().Remove(entity);
            DIKernel.Get<IDoctorRepository>().Delete(entity.Doctor);
            for (int i = 0; i < _entities.Count; i++)
            {
                Patient patient = _entities[i];
                if (patient.Id == entity.Id)
                {
                    _entities.RemoveAt(i);
                }
            }
            return true;
        }

        public override bool Save(Patient entity)
        {
            var role_repo = DIKernel.Get<IRoleRepository>();
            entity.Role = role_repo.GetByName("Patient");
            var user_repo = DIKernel.Get<IUserRepository>();
            entity.Doctor = DIKernel.Get<IDoctorRepository>().GetById(entity.Doctor.Id);
            user_repo.Save(entity);
            return base.Save(entity);
        }

        public Patient GetById(int patient_id)
        {
            return _entities.Where(p => p.Id == patient_id).FirstOrDefault();
        }

        public bool DeleteById(int patient_id)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                Patient patient = _entities[i];
                if (patient.Id == patient_id)
                {
                    _entities.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public Patient GetByIdIfNotLedDoctor(int patient_id, int doctor_id)
        {
            return _entities.Where(p => p.Id == patient_id && p.Doctor.Id != doctor_id).FirstOrDefault();
        }
    }
}
