using System.Collections.Generic;
using System.Linq;

namespace Health.Core.Entities.Virtual
{
    public static class DaysInWeek
    {
        private static readonly Dictionary<int, Day> _days = new Dictionary<int, Day>
                                                                 {
                                                                     {1, new Day {Name = "Понедельник", Number = 1}},
                                                                     {2, new Day {Name = "Вторник", Number = 2}},
                                                                     {3, new Day {Name = "Среда", Number = 3}},
                                                                     {4, new Day {Name = "Четверг", Number = 4}},
                                                                     {5, new Day {Name = "Пятница", Number = 5}},
                                                                     {6, new Day {Name = "Суббота", Number = 6}},
                                                                     {7, new Day {Name = "Воскресенье", Number = 7}},
                                                                     {8, new Day {Name = "Все", Number = 8}}
                                                                 };

        public static readonly Day Month = _days[1];

        public static readonly Day Tuesday = _days[2];

        public static readonly Day Wednesday = _days[3];

        public static readonly Day Thursday = _days[4];

        public static readonly Day Friday = _days[5];

        public static readonly Day Saturday = _days[6];

        public static readonly Day Sunday = _days[7];

        public static readonly Day All = _days[8];

        public static Day Day(int index)
        {
            return (index <= _days.Count & index > 0) ? _days[index] : null;
        }

        public static Day Day(string name)
        {
            return (from day in _days
                    where day.Value.Name == name
                    select day.Value).FirstOrDefault();
        }
    }
}