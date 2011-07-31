using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Health.API.Services;
using Health.Site.Models;
using Ninject;

namespace Health.Site.Controllers
{
    public class AccountController : CoreController
    {
        public AccountController(IKernel di_kernel) : base(di_kernel)
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
        public ActionResult Login(LoginFormModel login_form_model)
        {
            if (ModelState.IsValid)
            {
                if (CoreKernel.AuthServ.Login(login_form_model.Login, login_form_model.Password,
                                            login_form_model.RememberMe))
                {
                    return RedirectToRoute("Admin");
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

        /// <summary>
        /// Можно удалить 
        /// </summary>
        /// <returns></returns>
        public ActionResult Cookie()
        {
            var ticket = new FormsAuthenticationTicket(0, "Somename", DateTime.Now, DateTime.Now.AddHours(1), true, "");
            string tk = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie("SomeName", tk)
                             {
                                 Expires = DateTime.Now.AddHours(1)
                             };
            Response.Cookies.Add(cookie);
            return RedirectToRoute("Home");
        }
    }
}