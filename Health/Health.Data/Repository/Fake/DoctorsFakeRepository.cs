using System;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class DoctorsFakeRepository : CoreFakeRepository<Doctor>, IDoctorRepository
    {
        public DoctorsFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            Save(new Doctor
                     {
                         FirstName = "Анатолий",
                         LastName = "Петров",
                         ThirdName = "Витальевич",
                         Login = "doctor",
                         Password = "doctor",
                         Role = CoreKernel.RoleRepo.GetByName("Doctor"),
                         Birthday = DateTime.Now,
                         Specialty = DIKernel.Get<ISpecialtyRepository>().GetById(1)
                     });

            Save(new Doctor
                     {
                         FirstName = "Анатолий1",
                         LastName = "Петров1",
                         ThirdName = "Витальевич1",
                         Login = "doctor1",
                         Password = "doctor1",
                         Role = CoreKernel.RoleRepo.GetByName("Doctor"),
                         Birthday = DateTime.Now,
                         Specialty = DIKernel.Get<ISpecialtyRepository>().GetById(2)
                     });
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
    }
}
