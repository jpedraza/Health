using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Site.Controllers;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Areas.Parameters.Models;

namespace Health.Site.Areas.Parameters.Controllers
{
    public class AddController : CoreController
    {
        public AddController(IDIKernel di_kernel)
            : base(di_kernel)
        {
        }
        //
        // GET: /Parameters/Add/

        /// <summary>
        /// Отображение формы начала создания
        /// </summary>
        [PRGImport]
        public ActionResult Index([Bind(Include = "StartAddForm")] ParametersViewModel form_model)
        {
            if (form_model != null && form_model.StartAddForm != null)
            {
                return View(form_model);
            }
            return View();
        }

    }
}
