using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public class PersonalScheduleFakeRepository : CoreFakeRepository<PersonalSchedule>, IPersonalScheduleRepository
    {
        public PersonalScheduleFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
        }
    }
}
