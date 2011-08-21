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
using Health.Site.Extensions;

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

        [HttpPost, ValidateAntiForgeryToken, PRGExport(ParametersHook = true)]
        public ActionResult InterviewSubmit([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                return RedirectTo<InterviewController>(a => a.Confirm());
            }
            return RedirectTo<InterviewController>(a => a.Interview(form_model));
        }

        public string Confirm()
        {
            return "Confirm";
        }
    }
}
