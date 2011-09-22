using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.API.Repository.Abstract;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IPersonalScheduleRepository : ICoreRepository<PersonalSchedule>, IScheduleRepository
    {
        PersonalSchedule GetById(int schedule_id);

        bool DeleteById(int schedule_id);
    }
}
