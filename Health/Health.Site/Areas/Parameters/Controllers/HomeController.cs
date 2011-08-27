using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Controllers;

namespace Health.Site.Areas.Parameters
{
    public class HomeController : CoreController
    {
        public HomeController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        //
        // GET: /Parameters/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
