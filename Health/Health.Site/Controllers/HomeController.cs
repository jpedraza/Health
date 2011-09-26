﻿using System.Web.Mvc;
using Health.Core.API;

namespace Health.Site.Controllers
{
    public class HomeController : CoreController
    {
        public HomeController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Добро пожаловать в ASP.NET MVC!";
            return View();
        }
    }
}