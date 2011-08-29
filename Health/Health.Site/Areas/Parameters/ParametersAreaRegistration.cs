using System.Web.Mvc;
using System.Linq;

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
                "Parameters/{controller}/{action}",
                new { controller = "Home", action = "Index"}
            );

        }
    }
}
