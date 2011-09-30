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
        public PatientFakeRepository(IDIKernel diKernel) : base(diKernel)
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
            var roleRepo = DIKernel.Get<IRoleRepository>();
            entity.Role = roleRepo.GetByName("Patient");
            var userRepo = DIKernel.Get<IUserRepository>();
            entity.Doctor = DIKernel.Get<IDoctorRepository>().GetById(entity.Doctor.Id);
            userRepo.Save(entity);
            return base.Save(entity);
        }

        public Patient GetById(int patientId)
        {
            return _entities.Where(p => p.Id == patientId).FirstOrDefault();
        }

        public bool DeleteById(int patientId)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                Patient patient = _entities[i];
                if (patient.Id == patientId)
                {
                    _entities.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public Patient GetByIdIfNotLedDoctor(int patientId, int doctorId)
        {
            return _entities.Where(p => p.Id == patientId && p.Doctor.Id != doctorId).FirstOrDefault();
        }
    }
}
