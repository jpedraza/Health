using System.Collections.Generic;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using Health.Site.Models;
using Health.Site.Models.Metadata;
using System.Web.Mvc;
using System.ComponentModel;

namespace Health.Site.Areas.Admin.Models
{
    public class PatientForm : CoreViewModel
    {
        [ClassMetadata(typeof(PatientFormMetadata))]
        public virtual Patient Patient { get; set; }

        public IEnumerable<Doctor> Doctors { get; set; }

        public IEnumerable<SelectListItem> DoctorSelectList 
        {
            get
            {
                var selectList = new BindingList<SelectListItem>();
                foreach (Doctor doctor in Doctors)
                {
                    selectList.Add(new SelectListItem 
                    { 
                        Selected = Patient != null && Patient.Doctor != null && doctor.Id == Patient.Doctor.Id,
                        Text = doctor.FullName,
                        Value = doctor.Id.ToString()
                    });
                }
                return selectList;
            }
        }

        public string Message { get; set; }
    }

    public class PatientList : CoreViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }
    }
}