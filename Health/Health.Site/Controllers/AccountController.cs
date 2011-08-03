using System.Collections.Generic;
using System.Web.Mvc;
using Health.API;
using Health.Data.Entities;
using Health.Site.Models;
using Health.Site.Models.Forms;

namespace Health.Site.Controllers
{
    public class AccountController : CoreController
    {
        public AccountController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        /// <summary>
        /// Отображение формы опроса
        /// </summary>
        /// <returns></returns>
        public ActionResult Interview()
        {
            var form_model = new InterviewFormModel(DIKernel)
                                 {
                                     Parameters = new List<Parameter>
                                                      {
                                                          new Parameter
                                                              {
                                                                  Name = "P1",
                                                                  Value = "V1"
                                                              },
                                                          new Parameter
                                                              {
                                                                  Name = "P2",
                                                                  Value = "V2"
                                                              }
                                                      }
                                 };
            var acc_view_model = new AccountViewModel
                                     {
                                         InterviewForm = form_model
                                     };
            return View(acc_view_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Interview([Bind(Include = "InterviewForm")] AccountViewModel form_model)
        {
            if (ModelState.IsValid)
            {
            }
            return View(form_model);
        }
    }
}