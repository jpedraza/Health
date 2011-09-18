using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Areas.Account.Models;
using Health.Site.Attributes;
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
        [PRGImport(ParametersHook = true)]
        public ActionResult Registration(RegistrationForm form)
        {
            return View(form);
        }

        /// <summary>
        /// Обработка запроса на регистрацию
        /// </summary>
        /// <param name="form">Модель формы регистрации</param>
        [HttpPost, PRGExport(ParametersHook = true)]
        public ActionResult RegistrationSubmit(RegistrationForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.RegServ.SaveBid(form.Candidate);
                return RedirectTo<HomeController>(a => a.Index());
            }
            return RedirectTo<RegistrationController>(a => a.Registration(form));
        }
    }
}