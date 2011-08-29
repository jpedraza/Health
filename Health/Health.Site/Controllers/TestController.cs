using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TestModel test_model)
        {
            if (ModelState.IsValid){}
            return View(test_model);
        }
    }
}
