using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Site.Controllers;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Areas.Parameters.Models;
using Health.Core.Entities.POCO;

namespace Health.Site.Areas.Parameters.Controllers
{
    public class EditingController : CoreController
    {
        public EditingController(IDIKernel di_kernel)
            : base(di_kernel)
        {
        }
        
        //
        // GET: /Parameters/Edit/

        public ActionResult Index()
        {
            ViewData["Parameters"] = CoreKernel.ParamRepo.GetAllParam();
            return View();
        }

        //
        // GET: /Parameters/Editing/Edit
        //TODO: сделать страницу и сообщения пользователю об ошибке.
        public ActionResult Edit(int parameter_id)
        {
            if (parameter_id != 0)
            {
                ParametersViewModel form_model = new ParametersViewModel();
                form_model.StartAddForm = new Models.Forms.StartAddFormModel();
                Parameter parameter = CoreKernel.ParamRepo.GetById(parameter_id);
                if (parameter != null)
                {
                    form_model.StartAddForm.DefaultValue = parameter.DefaultValue.ToString();
                    form_model.StartAddForm.Is_childs = parameter.MetaData.Is_childs;
                    form_model.StartAddForm.Is_var = parameter.MetaData.Is_var;
                    form_model.StartAddForm.Name = parameter.Name;
                    form_model.StartAddForm.Value = parameter.Value.ToString();
                    form_model.StartAddForm.Is_param = (parameter.MetaData.Id_parent != null);
                    return View(form_model);
                }
            }
            return View();
        }

        public ActionResult Edit2([Bind(Include = "StartAddForm")] ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                if (form_model != null && form_model.StartAddForm != null)
                {
                    form_model =  form_model.StartAddParameter(form_model.StartAddForm.Name, 
                        form_model.StartAddForm.Value, 
                        form_model.StartAddForm.DefaultValue, 
                        form_model.StartAddForm.Is_childs, 
                        form_model.StartAddForm.Is_var, 
                        form_model.StartAddForm.Is_param, 
                        CoreKernel.ParamRepo);
                    TempData["NewParam"] = form_model.NewParam;
                    return View(form_model);
                }
                else
                    return RedirectTo<EditingController>(a => a.Index());
            }
            return View();
        }
    }
}
