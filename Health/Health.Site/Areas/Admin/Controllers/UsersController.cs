using System;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Admin.Models.Users;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class UsersController : CoreController
    {
        public UsersController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            var model = new UsersList
                            {
                                Users = CoreKernel.UserRepo.GetAll(),
                                Roles = CoreKernel.RoleRepo.GetAll()
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(UsersList form)
        {
            string controller_name = String.Format("{0}s", form.Role.Name);
            const string action_name = "Add";
            return RedirectToRoute(new {area = "Admin", controller = controller_name, action = action_name});
        }
    }
}
