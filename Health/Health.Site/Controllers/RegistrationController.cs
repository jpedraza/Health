using System.Web.Mvc;
using Health.API.Services;
using Health.Site.Models;
using Ninject;

namespace Health.Site.Controllers
{
    public class RegistrationController : CoreController
    {
        public RegistrationController(IKernel di_kernel) : base(di_kernel)
        {
        }

        /// <summary>
        /// Отображение формы регистрации
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
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
        public ActionResult Index(CandidateRegistrationFormModel form_model)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.RegServ.SaveBid(form_model);
                return RedirectToRoute("Home");
            }
            return View(form_model);
        }
    }
}