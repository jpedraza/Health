using System.Web.Mvc;

namespace Health.Site.Areas.Parameters
{
    public class ParametersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Parameters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Parameters_default",
                "Parameters/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Health.Site.Areas.Parameters.Controllers" }
            );
        }
    }
}
