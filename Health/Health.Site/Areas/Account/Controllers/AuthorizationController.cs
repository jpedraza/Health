using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Account.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;
using MvcContrib;

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
        [PRGImport]
        public ActionResult Login([Bind(Include = "LoginForm")] AccountViewModel form_model)
        {
            if (form_model != null && form_model.LoginForm != null)
            {
                return View(form_model);
            }
            return View();
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="form_model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, PRGExport]
        public ActionResult LoginSubmit([Bind(Include = "LoginForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                if (CoreKernel.AuthServ.Login(form_model.LoginForm.Login, form_model.LoginForm.Password,
                                              form_model.LoginForm.RememberMe))
                {
                    return RedirectToRoute(new {area = "Admin", controller = "Home", action = "Index"});
                }
            }
            return this.RedirectToAction(a => a.Login(form_model));
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            CoreKernel.AuthServ.Logout();

            return RedirectToRoute(new {area = "", controller = "Home", action = "Index"});
        }
    }
}