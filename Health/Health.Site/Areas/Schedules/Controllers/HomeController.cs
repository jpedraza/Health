using System.Web.Mvc;
using Health.Site.Attributes;

namespace Health.Site.Areas.Schedules.Controllers
{
    [Auth(AllowRoles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
