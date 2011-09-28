using System;
using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class DoctorsFakeRepository : CoreFakeRepository<Doctor>, IDoctorRepository
    {
        public DoctorsFakeRepository(IDIKernel di_kernel) : base(di_kernel)
        {
            
        }

        #region Implementation of IDoctorRepository

        public Doctor GetById(int doctor_id)
        {
            return _entities.Where(e => e.Id == doctor_id).FirstOrDefault();
        }

        public bool DeleteById(int doctor_id)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                Doctor doctor = _entities[i];
                if (doctor_id == doctor.Id)
                {
                    if (doctor.Patients.Count() != 0)
                    {
                        throw new Exception("Нельзя оставить пациентов без доктора. Для начала назначте пациентам нового лечащего врача.");
                    }
                    _entities.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        #endregion

        public override bool Save(Doctor entity)
        {
            entity.Specialty = DIKernel.Get<ISpecialtyRepository>().GetById(entity.Specialty.Id);
            return base.Save(entity);
        }

        public override bool Update(Doctor entity)
        {
            entity.Specialty = DIKernel.Get<ISpecialtyRepository>().GetById(entity.Specialty.Id);
            return base.Update(entity);
        }

        public Doctor GetByIdIfNotLedPatient(int doctor_id, int patient_id)
        {
            return _entities.Where(
                        d => d.Id == doctor_id && 
                        d.Patients.Where(
                            p => p.Id == patient_id).FirstOrDefault() == null).FirstOrDefault();
        }

        public override bool Delete(Doctor entity)
        {
            entity = _entities.Where(e => e.Id == entity.Id).FirstOrDefault();
            if (entity != null && entity.Patients.Count() != 0)
            {
                throw new Exception("Нельзя оставить пациентов без доктора. Для начала назначте пациентам нового лечащего врача.");
            }
            return base.Delete(entity);
        }
    }
}
