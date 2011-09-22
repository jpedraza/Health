using System.Web.Mvc;
using Health.Core.API;
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
        
        public ActionResult Index()
        {
            return View();
        }
    }
}