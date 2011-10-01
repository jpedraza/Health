using Health.Core.API.Repository.Abstract;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IPersonalScheduleRepository : ICoreRepository<PersonalSchedule>, IScheduleRepository
    {
        PersonalSchedule GetById(int scheduleId);

        bool DeleteById(int scheduleId);
    }
}
