using System.Web.Mvc;
using Health.API;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class HomeController : CoreController
    {
        //
        // GET: /Admin/Home/

        public HomeController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        [Auth(AllowRoles = "Admin")]
        public ActionResult Index()
        {
            ViewData["Role"] = CoreKernel.AuthServ.UserCredential.Role;
            return View();
        }
    }
}