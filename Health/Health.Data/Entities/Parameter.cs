using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.API.Entities;

namespace Health.Data.Entities
{
    public class Parameter : IParameter
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public IMetaData[] MetaData { get; set; }
    }
}
