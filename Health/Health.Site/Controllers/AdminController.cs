using System.Web.Mvc;
using Health.API;
using Health.Site.Attributes;

namespace Health.Site.Controllers
{
    public class AdminController : CoreController
    {
        public AdminController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        [Auth(AllowRoles = "Admin")]
        public ActionResult Index()
        {
            ViewData["Role"] = CoreKernel.AuthServ.UserCredential.Role;
            return View(model: "hello");
        }
    }
}