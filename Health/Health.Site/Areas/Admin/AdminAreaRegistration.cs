using System;
using System.Web.Mvc;
using Health.Site.Attributes;

namespace Health.Site.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default_date",
                "Admin/Doctor/{pre_action}/{post_action}/{id}/{year}/{month}/{day}",
                new { controller = "Doctor", format = "{0}/{1}", year = DateTime.Now.Year, month = DateTime.Now.Month, day = DateTime.Now.Day },
                new { controller = "Doctor", pre_action = "Schedule|Appointment", post_action = "Show|Edit" }
                ).RouteHandler = new FormatActionName();

            context.MapRoute(
                "Admin_delete",
                "Admin/{controller}/Delete/{id}/{confirm}",
                new {action = "Delete", id = UrlParameter.Optional, confirm = UrlParameter.Optional}
                );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}