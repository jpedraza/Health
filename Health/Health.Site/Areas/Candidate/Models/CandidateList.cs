using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.API;
using Health.Site.Models;

namespace Health.Site.Areas.Candidate.Models
{
    public class CandidateList : CoreViewModel
    {
        public IEnumerable<Core.Entities.POCO.Candidate> Candidates { get; set; }
    }
}