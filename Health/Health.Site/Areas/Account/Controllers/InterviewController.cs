using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.API;
using Health.Data.Entities;
using Health.Site.Areas.Account.Models;
using Health.Site.Areas.Account.Models.Forms;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Health.Site.DI;

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
        [PRGImport]
        public ActionResult Interview()
        {
            var form_model = TempData["model"] as InterviewFormModel ?? new InterviewFormModel(DIKernel)
                                                                            {
                                                                                Parameters = new List<Parameter>
                                                                                                 {
                                                                                                     new Parameter
                                                                                                         {
                                                                                                             Name = "P1",
                                                                                                             Value =
                                                                                                                 "V1"
                                                                                                         },
                                                                                                     new Parameter
                                                                                                         {
                                                                                                             Name = "P2",
                                                                                                             Value =
                                                                                                                 "V2"
                                                                                                         }
                                                                                                 }
                                                                            };
            var acc_view_model = new AccountViewModel
                                     {
                                         InterviewForm = form_model
                                     };
            return View(acc_view_model);
        }

        [HttpPost, ValidateAntiForgeryToken, PRGExport]
        public ActionResult Interview([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                return RedirectTo<InterviewController>(a => a.Confirm());
            }
            return RedirectTo<InterviewController>(a => a.Interview(), form_model.InterviewForm);
        }

        public string Confirm()
        {
            return "Confirm";
        }

    }
}
