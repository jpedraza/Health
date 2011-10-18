using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using Health.Site.Models;
using Health.Site.Models.Metadata;

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
                var selectList = new BindingList<SelectListItem>();
                foreach (Specialty specialty in Specialties)
                {
                    selectList.Add(new SelectListItem
                                        {
                                            Text = specialty.Name,
                                            Value = specialty.Id.ToString()
                                        });
                }
                return selectList;
            }
        }

        public string Message { get; set; }
    }

    public class ScheduleForm : CoreViewModel
    {
        public WorkWeek WorkWeek { get; set; }
    }

    public class AppointmentList : CoreViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}