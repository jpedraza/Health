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
        public DefaultScheduleFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            _entities = new List<DefaultSchedule>
                            {
                                new DefaultSchedule
                                    {
                                        Day = DaysInWeek.All,
                                        Week = WeeksInMonth.All,
                                        Month = MonthsInYear.All,
                                        Parameter = new Parameter
                                                        {
                                                            Name = "давление"
                                                        },
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
                                        Parameter = new Parameter
                                                        {
                                                            Name = "температура"
                                                        },
                                        Period = new Period
                                                     {
                                                         Months = 6
                                                     },
                                        TimeStart = new TimeSpan(10, 0, 0),
                                        TimeEnd = new TimeSpan(22, 0, 0)
                                    }
                            };
        }
    }
}
