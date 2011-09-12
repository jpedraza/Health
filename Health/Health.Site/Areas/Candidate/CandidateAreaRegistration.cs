using System.Web.Mvc;

namespace Health.Site.Areas.Candidate
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
                "Candidate_default",
                "Candidate/{controller}/{action}",
                new {controller = "Home", action = "Index"}
                );

            context.MapRoute(
                "Candidate_Crud_RejectBid",
                "Candidate/Crud/RejectBid/{candidate_id}",
                new { controller = "Crud", action = "RejectBid", candidate_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Candidate_Crud_AcceptBid",
                "Candidate/Crud/AcceptBid/{candidate_id}",
                new { controller = "Crud", action = "AcceptBid", candidate_id = UrlParameter.Optional }
                );
        }
    }
}
