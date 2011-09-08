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
                "Schedule/Default/Edit/{schedule_id}",
                new { controller = "Default", action = "Edit", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedule_Default_Delete",
                "Schedule/Default/Delete/{schedule_id}",
                new { controller = "Default", action = "Delete", schedule_id = UrlParameter.Optional }
                );
        }
    }
}
