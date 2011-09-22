using System.Collections.Generic;
using Health.Site.Models;

namespace Health.Site.Areas.Candidates.Models
{
    public class CandidateList : CoreViewModel
    {
        public IEnumerable<Core.Entities.POCO.Candidate> Candidates { get; set; }
    }
}