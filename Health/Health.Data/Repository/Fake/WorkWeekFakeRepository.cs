using System;
using System.Collections.ObjectModel;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;

namespace Health.Data.Repository.Fake
{
    public sealed class WorkWeekFakeRepository : CoreFakeRepository<WorkWeek>, IWorkWeekRepository
    {
        public WorkWeekFakeRepository(IDIKernel diKernel) : base(diKernel)
        {
            
        }

        #region Implementation of IWorkWeekRepository

        /// <summary>
        /// Получить рабочую неделю для доктора.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <returns>Рабочая неделя или null если рабочая неделя не определена.</returns>
        public WorkWeek GetByDoctorId(int doctorId)
        {
            return _entities.Where(e => e.Doctor.Id == doctorId).FirstOrDefault();
        }

        #endregion

        public override bool Update(WorkWeek entity)
        {
            foreach (WorkWeek t in _entities)
            {
                WorkWeek workWeek = t;
                if (workWeek.Doctor.Id == entity.Doctor.Id)
                {
                    for (int j = 0; j < entity.WorkDays.Count; j++)
                    {
                        t.WorkDays[j].TimeStart = entity.WorkDays[j].TimeStart;
                        t.WorkDays[j].TimeEnd = entity.WorkDays[j].TimeEnd;
                        t.WorkDays[j].AttendingHoursStart = entity.WorkDays[j].AttendingHoursStart;
                        t.WorkDays[j].AttendingHoursEnd = entity.WorkDays[j].AttendingHoursEnd;
                        t.WorkDays[j].DinnerStart = entity.WorkDays[j].DinnerStart;
                        t.WorkDays[j].DinnerEnd = entity.WorkDays[j].DinnerEnd;
                        t.WorkDays[j].IsWeekEndDay = entity.WorkDays[j].IsWeekEndDay;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
