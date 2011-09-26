using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
                         Id = 1,
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
                         Id = 2,
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

        #endregion
    }
}
