using System.Web.Mvc;
using Health.Site.Attributes;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class HomeController : Controller
    {
        //[Auth(AllowRoles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}