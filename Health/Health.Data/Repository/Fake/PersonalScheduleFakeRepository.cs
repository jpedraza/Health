using System;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;

namespace Health.Data.Repository.Fake
{
    public class PersonalScheduleFakeRepository : CoreFakeRepository<PersonalSchedule>, IPersonalScheduleRepository
    {
        public PersonalScheduleFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel)
            : base(di_kernel, core_kernel)
        {
            _entities.Add(new PersonalSchedule
                              {
                                  Parameter = DIKernel.Get<IParameterRepository>().GetById(1),
                                  Patient = DIKernel.Get<IPatientRepository>().GetById(1),
                                  DateStart = DateTime.Now,
                                  DateEnd = DateTime.Now.AddYears(1),
                                  Day = DaysInWeek.All,
                                  Month = MonthsInYear.All,
                                  Week = WeeksInMonth.All,
                                  TimeStart = new TimeSpan(10, 0, 0),
                                  TimeEnd = new TimeSpan(23, 0, 0)
                              });

            _entities.Add(new PersonalSchedule
                              {
                                  Parameter = DIKernel.Get<IParameterRepository>().GetById(2),
                                  Patient = DIKernel.Get<IPatientRepository>().GetById(2),
                                  DateStart = DateTime.Now,
                                  DateEnd = DateTime.Now.AddYears(2),
                                  Day = DaysInWeek.Monday,
                                  Month = MonthsInYear.All,
                                  Week = WeeksInMonth.Even,
                                  TimeStart = new TimeSpan(8, 0, 0),
                                  TimeEnd = new TimeSpan(12, 0, 0)
                              });
        }

        public PersonalSchedule GetById(int schedule_id)
        {
            return _entities.Where(e => e.Id == schedule_id).FirstOrDefault();
        }

        public override bool Save(PersonalSchedule entity)
        {
            entity.Patient = DIKernel.Get<IPatientRepository>().GetById(entity.Patient.Id);
            entity.Parameter = DIKernel.Get<IParameterRepository>().GetById(entity.Parameter.Id);
            return base.Save(entity);
        }

        public bool DeleteById(int schedule_id)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                PersonalSchedule entity = _entities[i];
                if (entity.Id == schedule_id)
                {
                    _entities.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public override bool Update(PersonalSchedule entity)
        {
            entity.Parameter = DIKernel.Get<IParameterRepository>().GetById(entity.Parameter.Id);
            entity.Patient = DIKernel.Get<IPatientRepository>().GetById(entity.Patient.Id);
            for (int i = 0; i < _entities.Count; i++)
            {
                PersonalSchedule schedule = _entities[i];
                if (schedule.Id == entity.Id)
                {
                    _entities[i] = entity;
                    return true;
                }
            }
            return false;
        }
    }
}