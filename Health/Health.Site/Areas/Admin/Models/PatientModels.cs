using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;
using Health.Site.Models.Metadata;
using Health.Site.Models.Providers;

namespace Health.Site.Areas.Admin.Models
{
    public class PatientForm : CoreViewModel
    {
        [ClassMetadata(typeof(PatientFormMetadata))]
        public Patient Patient { get; set; }

        public string Message { get; set; }
    }

    public class PatientList : CoreViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }
    }
}