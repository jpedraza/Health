using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Core.Entities.POCO.Abstract;
using Health.Core.Entities.Virtual;

namespace Health.Data.Repository.Fake
{
    public class DefaultScheduleFakeRepository : CoreFakeRepository<DefaultSchedule>, IDefaultScheduleRepository
    {
        public DefaultScheduleFakeRepository(IDIKernel diKernel) : base(diKernel)
        {
            Save(new DefaultSchedule
                {
                    Day = DaysInWeek.All,
                    Week = WeeksInMonth.All,
                    Month = MonthsInYear.All,
                    Parameter = DIKernel.Get<IParameterRepository>().GetById(1),
                    Period = new Period
                                    {
                                        Years = 2
                                    },
                    TimeStart = new TimeSpan(10, 0, 0),
                    TimeEnd = new TimeSpan(22, 0, 0)
                });
            Save(new DefaultSchedule
                {
                    Day = DaysInWeek.All,
                    Week = WeeksInMonth.Even,
                    Month = MonthsInYear.May,
                    Parameter = DIKernel.Get<IParameterRepository>().GetById(2),
                    Period = new Period
                                    {
                                        Months = 6
                                    },
                    TimeStart = new TimeSpan(10, 0, 0),
                    TimeEnd = new TimeSpan(22, 0, 0)
                });
        }

        public DefaultSchedule GetById(int scheduleId)
        {
            return (from defaultSchedule in _entities
                   where defaultSchedule.Id == scheduleId
                   select defaultSchedule).FirstOrDefault();
        }

        public bool DeleteById(int scheduleId)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                DefaultSchedule defaultSchedule = _entities[i];
                if (defaultSchedule.Id == scheduleId)
                {
                    _entities.RemoveAt(i);
                }
            }
            return true;
        }

        public override bool Update(DefaultSchedule entity)
        {
            foreach (DefaultSchedule defaultSchedule in _entities)
            {
                if (defaultSchedule.Id == entity.Id)
                {
                    _entities.Remove(defaultSchedule);
                    entity.Parameter = DIKernel.Get<IParameterRepository>().GetById(entity.Parameter.Id);
                    _entities.Add(entity);
                    return true;
                }
            }
            throw new Exception("Переданное для обновления дефолтное расписание отсутствует в репозитории.");
        }

        public override sealed bool Save(DefaultSchedule entity)
        {
            entity.Parameter = DIKernel.Get<IParameterRepository>().GetById(entity.Parameter.Id);
            return base.Save(entity);
        }
    }
}
