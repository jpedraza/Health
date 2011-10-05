using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Services;
using Health.Site.Areas.Account.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Account.Controllers
{
    public class RegistrationController : CoreController
    {
        public RegistrationController(IDIKernel diKernel) : base(diKernel)
        {
        }

        /// <summary>
        /// Отображение формы регистрации
        /// </summary>
        [PRGImport, ValidationModel]
        public ActionResult Registration(RegistrationForm form)
        {
            return View(form);
        }

        /// <summary>
        /// Обработка запроса на регистрацию
        /// </summary>
        /// <param name="form">Модель формы регистрации</param>
        [HttpPost, PRGExport, ValidationModel, ActionName("Registration")]
        public ActionResult RegistrationSubmit(RegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IRegistrationService>().SaveBid(form.Candidate);
                return RedirectTo<HomeController>(a => a.Index());
            }
            return RedirectTo<RegistrationController>(a => a.Registration(form));
        }
    }
}