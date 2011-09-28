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
        public DefaultScheduleFakeRepository(IDIKernel di_kernel) : base(di_kernel)
        {
            _entities = new List<DefaultSchedule>
                            {
                                new DefaultSchedule
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
                                    },
                                new DefaultSchedule
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
                                    }
                            };
        }

        public DefaultSchedule GetById(int schedule_id)
        {
            return (from default_schedule in _entities
                   where default_schedule.Id == schedule_id
                   select default_schedule).FirstOrDefault();
        }

        public bool DeleteById(int schedule_id)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                DefaultSchedule default_schedule = _entities[i];
                if (default_schedule.Id == schedule_id)
                {
                    _entities.RemoveAt(i);
                }
            }
            return true;
        }

        public override bool Update(DefaultSchedule entity)
        {
            foreach (DefaultSchedule default_schedule in _entities)
            {
                if (default_schedule.Id == entity.Id)
                {
                    _entities.Remove(default_schedule);
                    entity.Parameter = DIKernel.Get<IParameterRepository>().GetById(entity.Parameter.Id);
                    _entities.Add(entity);
                    return true;
                }
            }
            throw new Exception("Переданное для обновления дефолтное расписание отсутствует в репозитории.");
        }

        public override bool Save(DefaultSchedule entity)
        {
            entity.Parameter = DIKernel.Get<IParameterRepository>().GetById(entity.Parameter.Id);
            return base.Save(entity);
        }
    }
}
