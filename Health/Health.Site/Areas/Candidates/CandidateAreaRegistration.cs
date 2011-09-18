using System.Web.Mvc;

namespace Health.Site.Areas.Candidates
{
    public class CandidateAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Candidate"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Candidates_default",
                "Candidates/{controller}/{action}",
                new {controller = "Home", action = "Index"}
                );

            context.MapRoute(
                "Candidates_Crud_RejectBid",
                "Candidates/Crud/RejectBid/{candidate_id}",
                new { controller = "Crud", action = "RejectBid", candidate_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Candidates_Crud_AcceptBid",
                "Candidates/Crud/AcceptBid/{candidate_id}",
                new { controller = "Crud", action = "AcceptBid", candidate_id = UrlParameter.Optional }
                );
        }
    }
}
