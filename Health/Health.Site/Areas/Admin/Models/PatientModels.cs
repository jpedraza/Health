using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Admin.Models
{
    public class PatientForm : CoreViewModel
    {
        public Patient Patient { get; set; }
    }

    public class PatientList : CoreViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }
    }
}