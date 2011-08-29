using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Controllers;
using Health.Site.Areas.Parameters.Models;
using Health.Core.Entities.POCO;

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

        public ActionResult ShowAll()
        {
            return View(new ParametersViewModel
            { 
                parameters = CoreKernel.ParamRepo.GetAllParam()
            });
            //return View();
        }
    }
}
