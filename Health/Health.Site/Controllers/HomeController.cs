using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
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
            IEnumerable<Role> roles = CoreKernel.RoleRepo.GetAll();

            var new_role = new Role
                               {
                                   Name = "new_role",
                                   Code = 4323
                               };
            ViewBag.NewRole = new_role;

            ViewBag.VarName = new_role.PropertyName(x => x.Code);

            ViewData["Roles"] = roles;

            ViewBag.CountCandidates = CoreKernel.CandRepo.GetAll().Count();
            ViewBag.Message = "Добро пожаловать в ASP.NET MVC!";
            return View();
        }
    }
}