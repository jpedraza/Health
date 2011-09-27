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
        public PatientFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            Save(new Patient
            {
                Birthday = new DateTime(1980, 12, 2),
                Card = "some card number",
                FirstName = "patient1",
                LastName = "patient1",
                Login = "patient1",
                Password = "patient1",
                Policy = "some policy number",
                Role = di_kernel.Get<IRoleRepository>().GetByName("Patient"),
                ThirdName = "patient1"
            });
            Save(new Patient
            {
                Birthday = new DateTime(1980, 12, 2),
                Card = "some card number",
                FirstName = "patient2",
                LastName = "patient2",
                Login = "patient2",
                Password = "patient2",
                Policy = "some policy number",
                Role = di_kernel.Get<IRoleRepository>().GetByName("Patient"),
                ThirdName = "patient2"
            });
        }

        public override bool Delete(Patient entity)
        {
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
    }
}
