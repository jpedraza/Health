using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Admin.Models
{
    public class CandidateForm : CoreViewModel
    {
        public Candidate Candidate { get; set; }

        public string Message { get; set; }
    }

    public class CandidateList : CoreViewModel
    {
        public IEnumerable<Candidate> Candidates { get; set; }
    }
}