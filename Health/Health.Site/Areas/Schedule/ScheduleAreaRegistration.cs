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
        }
    }
}
