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
                "Schedules_Default_Show",
                "Schedules/Default/Show/{schedule_id}",
                new { controller = "Default", action = "Show", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Personal_Show",
                "Schedules/Personal/Show/{schedule_id}",
                new { controller = "Personal", action = "Show", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Default_AddBase",
                "Schedules/Default/Add/{schedule_id}",
                new { controller = "Default", action = "Add", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Personal_AddBase",
                "Schedules/Personal/Add/{schedule_id}",
                new { controller = "Personal", action = "Add", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Default_Edit",
                "Schedules/Default/Edit/{schedule_id}",
                new { controller = "Default", action = "Edit", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Personal_Edit",
                "Schedules/Personal/Edit/{schedule_id}",
                new { controller = "Personal", action = "Edit", schedule_id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Default_Delete",
                "Schedules/Default/Delete/{schedule_id}/{confirm}",
                new { controller = "Default", action = "Delete", schedule_id = UrlParameter.Optional, confirm = UrlParameter.Optional }
                );

            context.MapRoute(
                "Schedules_Personal_Delete",
                "Schedules/Personal/Delete/{schedule_id}/{confirm}",
                new { controller = "Personal", action = "Delete", schedule_id = UrlParameter.Optional, confirm = UrlParameter.Optional }
                );
        }
    }
}
