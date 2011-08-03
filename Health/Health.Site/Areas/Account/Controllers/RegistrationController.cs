using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.API;
using Health.Core;
using Health.Site.Controllers;
using Health.Site.Models;

namespace Health.Site.Areas.Account.Controllers
{
    public class RegistrationController : CoreController
    {
        public RegistrationController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        /// <summary>
        /// Отображение формы регистрации
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Обработка запроса на регистрацию
        /// </summary>
        /// <param name="form_model">Модель формы регистрации</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "RegistrationForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.RegServ.SaveBid(form_model.RegistrationForm);
                return RedirectToRoute("Home");
            }
            return View(form_model);
        }

    }
}
