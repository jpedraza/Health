using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Site.Models;
using Health.Site.Models.Metadata;
using Health.Site.Models.Providers;

namespace Health.Site.Areas.Admin.Models
{
    public class DoctorList : CoreViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
    }

    public class DoctorForm : CoreViewModel
    {
        [ClassMetadata(typeof(DoctorFormMetadata))]
        public Doctor Doctor { get; set; }

        public IEnumerable<Patient> Patients = new BindingList<Patient>();

        public IEnumerable<Specialty> Specialties = new BindingList<Specialty>();

        public IEnumerable<SelectListItem> SpecialitiesSelectList
        {
            get
            {
                var select_list = new BindingList<SelectListItem>();
                foreach (Specialty specialty in Specialties)
                {
                    select_list.Add(new SelectListItem
                                        {
                                            Text = specialty.Name,
                                            Value = specialty.Id.ToString()
                                        });
                }
                return select_list;
            }
        }

        public string Message { get; set; }
    }
}