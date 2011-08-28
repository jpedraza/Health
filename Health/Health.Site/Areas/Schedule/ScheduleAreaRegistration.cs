using System.Security.Policy;
using System.Web.Mvc;

namespace Health.Site.Areas.Schedule
{
    public class ScheduleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Schedule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Schedule_default",
                "Schedule/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "Schedule_Default_Edit",
                "Schedule/Default/Edit/{parameter_id}",
                new { controller = "Default", action = "Edit", parameter_id = UrlParameter.Optional }
                );
        }
    }
}
