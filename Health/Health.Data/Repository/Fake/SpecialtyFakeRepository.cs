using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class SpecialtyFakeRepository : CoreFakeRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            Save(new Specialty
                     {
                         Name = "Педиатор"
                     });
            Save(new Specialty
                     {
                         Name = "Кардиолог"
                     });
            Save(new Specialty
                     {
                         Name = "Стоматолог"
                     });
        }

        #region Implementation of ISpecialtyRepository

        public Specialty GetById(int specialty_id)
        {
            return _entities.Where(e => e.Id == specialty_id).FirstOrDefault();
        }

        #endregion
    }
}
