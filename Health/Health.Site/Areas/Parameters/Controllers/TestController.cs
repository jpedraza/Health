using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Site.DI;
using Health.Site.Controllers;
using Health.Core.API;
using Health.Data.Repository.Fake;
using Health.Site.Helpers;

namespace Health.Site.Areas.Parameters.Controllers
{
    public class TestController : CoreController
    {
        public TestController(IDIKernel di_kernel) : base(di_kernel) { }
        //
        // GET: /Parameters/Test/

        public ActionResult Index()
        {
            //Выберем два параметра для отрисовки.
            ViewData["parameter"] = DIKernel.Get<ParametersFakeRepository>().GetById(2);
            ViewData["parameter2"] = DIKernel.Get<ParametersFakeRepository>().GetById(6);

            ViewData["text"] = new HtmlString("<select><option>Some text</option><option>Some text</option></select>");
            return View();
        }

    }
}
