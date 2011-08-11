using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Site.Extensions;

namespace Health.Site.Controllers
{
    public class HomeController : CoreController
    {
        public HomeController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            IEnumerable<IRole> roles = CoreKernel.RoleRepo.GetAll();

            var new_role = Instance<IRole>(o =>
                                               {
                                                   o.Name = "new_role";
                                                   o.Code = 4323;
                                               });
            ViewBag.NewRole = new_role;

            ViewBag.VarName = new_role.PropertyName(x => x.Code);

            ViewData["Roles"] = roles;

            ViewBag.CountCandidates = CoreKernel.CandRepo.GetAll().Count();

            ViewBag.Message = "Добро пожаловать в ASP.NET MVC!";
            return View();
        }
    }
}