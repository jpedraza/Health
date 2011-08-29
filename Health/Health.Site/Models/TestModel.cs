using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;

namespace Health.Site.Models
{
    public class TestModel : CoreViewModel
    {
        public Patient Patient { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public DateTime Date { get; set; }
    }
}