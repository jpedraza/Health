using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.API;
using Health.Site.Controllers;
using Health.Site.Models;

namespace Health.Site.Areas.Account.Controllers
{
    public class AuthorizationController : CoreController
    {
        public AuthorizationController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        /// <summary>
        /// Отображение формы входа
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login_form_model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "LoginForm")] AccountViewModel login_form_model)
        {
            if (ModelState.IsValid)
            {
                if (CoreKernel.AuthServ.Login(login_form_model.LoginForm.Login, login_form_model.LoginForm.Password,
                                              login_form_model.LoginForm.RememberMe))
                {
                    return RedirectToRoute(new { area = "Admin", controller = "Home", action = "Index" });
                }
            }
            return View(login_form_model);
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            CoreKernel.AuthServ.Logout();

            return RedirectToRoute("Home");
        }

    }
}
