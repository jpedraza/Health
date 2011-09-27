using System.Web.Mvc;

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
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            context.MapRoute(
                "Admin_delete",
                "Admin/{controller}/Delete/{id}/{confirm}",
                new {action = "Delete", id = UrlParameter.Optional, confirm = UrlParameter.Optional}
                );
        }
    }
}