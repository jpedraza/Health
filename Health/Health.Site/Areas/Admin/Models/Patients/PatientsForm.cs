using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Admin.Models.Patients
{
    public class PatientsForm : CoreViewModel
    {
        public Patient Patient { get; set; }
    }
}