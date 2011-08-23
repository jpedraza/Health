using System.Collections.Generic;
using System.Linq;

namespace Health.Core.Entities.Virtual
{
    public static class WeeksInMonth
    {
        private static readonly Dictionary<int, Week> _weeks = new Dictionary<int, Week>
                                                                   {
                                                                       {1, new Week {Name = "Нечетная"}},
                                                                       {2, new Week {Name = "Четная"}},
                                                                       {3, new Week {Name = "Все"}}
                                                                   };

        public static readonly Week Odd = _weeks[1];
        public static readonly Week Even = _weeks[1];
        public static readonly Week All = _weeks[1];

        public static Week Week(int index)
        {
            return (index <= _weeks.Count & index > 0) ? _weeks[index] : null;
        }

        public static Week Week(string name)
        {
            return (from week in _weeks
                    where week.Value.Name == name
                    select week.Value).FirstOrDefault();
        }
    }
}