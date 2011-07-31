using System.Web.Mvc;
using Health.API.Services;
using Health.Site.Attributes;
using Ninject;

namespace Health.Site.Controllers
{
    public class AdminController : CoreController
    {
        public AdminController(IKernel di_kernel) : base(di_kernel)
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