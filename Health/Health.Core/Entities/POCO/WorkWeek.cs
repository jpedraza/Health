using System.Collections.Generic;
using Health.Core.Entities.POCO.Abstract;
using Health.Core.Entities.Virtual;

namespace Health.Core.Entities.POCO
{
    public class WorkWeek : IEntity
    {
        public Doctor Doctor { get; set; }

        public IList<WorkDay> WorkDays { get; set; }
    }
}
