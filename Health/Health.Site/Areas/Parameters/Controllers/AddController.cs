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

        [HttpPost]
        public ActionResult NextAdd([Bind(Include = "StartAddForm")] ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                
                if (form_model != null && form_model.StartAddForm != null)
                {
                    var Model =  form_model.StartAddParameter(form_model.StartAddForm.Name, form_model.StartAddForm.Value, form_model.StartAddForm.DefaultValue, form_model.StartAddForm.Is_childs, form_model.StartAddForm.Is_var, form_model.StartAddForm.Is_param, CoreKernel.ParamRepo);
                    return View(new ParametersViewModel { 
                    parameters = Model.parameters,
                    NewParam = Model.NewParam
                    });
                }
                else
                    RedirectTo<AddController>(a => a.Index(form_model));
            }

            return View();
        }
    }
}
