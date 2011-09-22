using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Account.Models;
using Health.Site.Areas.Account.Models.Forms;
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
        public ActionResult Interview([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (form_model != null && form_model.InterviewForm != null)
            {
                return View(form_model);
            }
            return View(new AccountViewModel()
                            {
                                InterviewForm = new InterviewFormModel()
                                                    {
                                                        Parameters = new List<Parameter>
                                                                         {
                                                                             new Parameter
                                                                                 {
                                                                                     Name = "P1",
                                                                                     Value = "V1"
                                                                                 }
                                                                         }
                                                    }
                            });
        }

        [HttpPost, ValidateAntiForgeryToken, PRGExport(ParametersHook = true)]
        public ActionResult InterviewSubmit([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                return RedirectTo<InterviewController>(a => a.Confirm());
            }
            return RedirectTo<InterviewController>(a => a.Interview(form_model));
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}