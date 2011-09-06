using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Core.Entities.Virtual
{
    [Serializable]
    public class Period
    {
        public int? Years { get; set; }

        public int? Months { get; set; }

        public int? Weeks { get; set; }

        public int? Days { get; set; }

        public int? Hours { get; set; }

        public int? Minutes { get; set; }
    }
}
