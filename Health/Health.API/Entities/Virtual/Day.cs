using System;
using Health.API.Extensions;

namespace Health.API.Entities.Virtual
{
    public class Day : IDay
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }
}
