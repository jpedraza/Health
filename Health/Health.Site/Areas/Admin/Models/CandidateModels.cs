using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using Health.Site.Models;
using Health.Site.Models.Metadata;

namespace Health.Site.Areas.Admin.Models
{
    public class CandidateForm : CoreViewModel
    {
        [ClassMetadata(typeof(CandidateFormMetadata))]
        public Candidate Candidate { get; set; }

        public string Message { get; set; }
    }

    public class CandidateAcceptForm : CoreViewModel
    {
        public Candidate Candidate { get; set; }

        public string Message { get; set; }

        [DisplayName("Лечащий врач")]
        [ClassMetadata(typeof(DoctorIdOnlyRequired))]
        public Doctor Doctor { get; set; }

        public IEnumerable<Doctor> Doctors = new BindingList<Doctor>();

        public IEnumerable<SelectListItem> DoctorsSelectList
        {
            get
            {
                var selectList = new BindingList<SelectListItem>();
                foreach (Doctor doctor in Doctors)
                {
                    selectList.Add(new SelectListItem
                                       {
                                           Text = doctor.FullName,
                                           Value = doctor.Id.ToString()
                                       });   
                }
                return selectList;
            }
        }
    }

    public class CandidateList : CoreViewModel
    {
        public IEnumerable<Candidate> Candidates { get; set; }
    }
}