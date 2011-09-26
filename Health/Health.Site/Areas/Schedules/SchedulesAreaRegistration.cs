using System.Web.Mvc;

namespace Health.Site.Areas.Schedules
{
    public class SchedulesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Schedules"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Schedules_default",
                "Schedules/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            context.MapRoute(
                "Schedules_Delete",
                "Schedules/{controller}/Delete/{id}/{confirm}",
                new {action = "Delete", id = UrlParameter.Optional, confirm = UrlParameter.Optional}
                );
        }
    }
}