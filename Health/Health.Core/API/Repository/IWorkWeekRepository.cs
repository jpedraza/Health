using System.Collections.Generic;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface IWorkWeekRepository : ICoreRepository<WorkWeek>
    {
        /// <summary>
        /// Получить рабочую неделю для доктора.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <returns>Рабочая неделя или null если рабочая неделя не определена.</returns>
        WorkWeek GetByDoctorId(int doctorId);
    }
}
