using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Account.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Account.Controllers
{
    public class InterviewController : CoreController
    {
        public InterviewController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        /// <summary>
        /// Отображение формы опроса
        /// </summary>
        /// <returns></returns>
        [PRGImport(ParametersHook = true)]
        public ActionResult Interview(InterviewForm form)
        {
            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, PRGExport(ParametersHook = true)]
        public ActionResult InterviewSubmit(InterviewForm form)
        {
            if (ModelState.IsValid)
            {
                return RedirectTo<InterviewController>(a => a.Confirm());
            }
            return RedirectTo<InterviewController>(a => a.Interview(form));
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}