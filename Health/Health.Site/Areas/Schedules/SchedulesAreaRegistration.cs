using System.Web.Mvc;

namespace Health.Site.Areas.Schedules
{
    public class SchedulesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Schedules";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Schedules_default",
                "Schedules/{controller}/{action}",
                new { controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "Schedules_Default_Edit",
                "Schedules/Default/Edit/{schedule_id}",
                new { controller = "Default", action = "Edit", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Default_Delete",
                "Schedules/Default/Delete/{schedule_id}",
                new { controller = "Default", action = "Delete", schedule_id = UrlParameter.Optional }
                );
        }
    }
}
