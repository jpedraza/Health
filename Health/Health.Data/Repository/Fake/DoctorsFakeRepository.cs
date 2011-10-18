using System;
using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;

namespace Health.Data.Repository.Fake
{
    public sealed class DoctorsFakeRepository : CoreFakeRepository<Doctor>, IDoctorRepository
    {
        public DoctorsFakeRepository(IDIKernel diKernel) : base(diKernel)
        {
            
        }

        #region Implementation of IDoctorRepository

        public Doctor GetById(int doctorId)
        {
            return _entities.Where(e => e.Id == doctorId).FirstOrDefault();
        }

        public bool DeleteById(int doctorId)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                Doctor doctor = _entities[i];
                if (doctorId == doctor.Id)
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
            entity.Specialty = Get<ISpecialtyRepository>().GetById(entity.Specialty.Id);
            base.Save(entity);
            Doctor doctor =
                _entities.Where(
                    d =>
                    d.FullName == entity.FullName && d.Login == entity.Login &&
                    d.Specialty.Name == entity.Specialty.Name).FirstOrDefault();
            if (doctor != null)
            {
                doctor.WorkWeek = doctor.WorkWeek ?? new WorkWeek
                                                         {
                                                             Doctor = doctor,
                                                             WorkDays = new List<WorkDay>
                                                                            {
                                                                                new WorkDay {Day = DaysInWeek.Monday},
                                                                                new WorkDay {Day = DaysInWeek.Tuesday},
                                                                                new WorkDay {Day = DaysInWeek.Wednesday},
                                                                                new WorkDay {Day = DaysInWeek.Thursday},
                                                                                new WorkDay {Day = DaysInWeek.Friday},
                                                                                new WorkDay {Day = DaysInWeek.Saturday},
                                                                                new WorkDay {Day = DaysInWeek.Sunday}
                                                                            }
                                                         };
                return Update(doctor);
            }
            return false;
        }

        public override bool Update(Doctor entity)
        {
            entity.Specialty = Get<ISpecialtyRepository>().GetById(entity.Specialty.Id);
            return base.Update(entity);
        }

        public Doctor GetByIdIfNotLedPatient(int doctorId, int patientId)
        {
            return _entities.Where(
                        d => d.Id == doctorId && 
                        d.Patients.Where(
                            p => p.Id == patientId).FirstOrDefault() == null).FirstOrDefault();
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
