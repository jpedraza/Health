using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Admin.Models
{
    public class DoctorList : CoreViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
    }

    public class DoctorForm : CoreViewModel
    {
        public Doctor Doctor { get; set; }
    }
}