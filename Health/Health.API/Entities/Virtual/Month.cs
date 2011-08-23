using System;
using Health.API.Extensions;

namespace Health.API.Entities.Virtual
{
    public class Month : IMonth
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }
}
