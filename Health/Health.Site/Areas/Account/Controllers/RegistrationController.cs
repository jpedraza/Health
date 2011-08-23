using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Account.Models;
using Health.Site.Controllers;

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
                return RedirectToRoute(new {area = "", controller = "Home", action = "Index"});
            }
            return View(form_model);
        }
    }
}