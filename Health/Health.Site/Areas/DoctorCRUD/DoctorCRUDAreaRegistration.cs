using System.Web.Mvc;

namespace Health.Site.Areas.DoctorCRUD
{
    public class DoctorCRUDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DoctorCRUD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DoctorCRUD_default",
                "Doctor/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
