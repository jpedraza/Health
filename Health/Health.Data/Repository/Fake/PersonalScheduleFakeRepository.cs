using System;
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
                                  Id = 1,
                                  Parameter = new Parameter
                                                  {
                                                      Id = 1
                                                  },
                                  Patient = new Patient
                                                {
                                                    Login = "Login 1"
                                                },
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
                                  Id = 2,
                                  Parameter = new Parameter
                                                  {
                                                      Id = 2
                                                  },
                                  Patient = new Patient
                                                {
                                                    Login = "Login 2"
                                                },
                                  DateStart = DateTime.Now,
                                  DateEnd = DateTime.Now.AddYears(2),
                                  Day = DaysInWeek.Monday,
                                  Month = MonthsInYear.All,
                                  Week = WeeksInMonth.Even,
                                  TimeStart = new TimeSpan(8, 0, 0),
                                  TimeEnd = new TimeSpan(12, 0, 0)
                              });
        }
    }
}
