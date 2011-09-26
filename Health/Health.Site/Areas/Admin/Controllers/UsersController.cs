using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class UsersController : CoreController
    {
        public UsersController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult List()
        {
            var model = new UserList
            {
                Users = CoreKernel.UserRepo.GetAll(),
                Roles = CoreKernel.RoleRepo.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(UserList form)
        {
            string controller = String.Format("{0}s", form.Role.Name);
            const string action = "Add";
            return RedirectToRoute(new {area = "Admin", controller, action});
        }
    }
}
