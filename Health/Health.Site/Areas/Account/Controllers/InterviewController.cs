using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Data.Entities;
using Health.Site.Areas.Account.Models;
using Health.Site.Areas.Account.Models.Forms;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Health.Site.DI;
using MvcContrib;
using MvcContrib.Filters;

namespace Health.Site.Areas.Account.Controllers
{
    [PassParametersDuringRedirect]
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
        public ActionResult Interview([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (form_model != null && form_model.InterviewForm != null)
            {
                return View(form_model);
            }
            return View(new AccountViewModel
                            {
                                InterviewForm = new InterviewFormModel(DIKernel)
                                                    {
                                                        Parameters = new List<IParameter>
                                                                         {
                                                                             Instance<IParameter>(o =>
                                                                                                      {
                                                                                                          o.Name = "P1";
                                                                                                          o.Value = "V1";
                                                                                                      }),
                                                                             Instance<IParameter>(o =>
                                                                                                      {
                                                                                                          o.Name = "P2";
                                                                                                          o.Value = "V2";
                                                                                                      })
                                                                         }
                                                    }
                            });
        }

        [HttpPost, ValidateAntiForgeryToken, PRGExport]
        public ActionResult InterviewSubmit([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                return this.RedirectToAction(a => a.Confirm());
            }
            return this.RedirectToAction(a => a.Interview(form_model));
        }

        public string Confirm()
        {
            return "Confirm";
        }
    }
}
