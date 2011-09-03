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
    public class AddController : CoreController
    {
        public AddController(IDIKernel di_kernel)
            : base(di_kernel)
        {
        }
        //
        // GET: /Parameters/Add/
        //TODO: Для добавления параметра необходимо 1-добавить проверку на null. 2 Добавить кнопки back
        //TODO: Добавить разметку на первой форме
        //TODO: PRG
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
                    TempData["NewParam"] = Model.NewParam;
                    return View(new ParametersViewModel { 
                    parameters = Model.parameters,
                    NewParam = Model.NewParam,
                    StartAddForm = Model.StartAddForm
                    });
                }
                else
                    return RedirectTo<AddController>(a => a.Index(form_model));
            }

            return View();
        }

        public ActionResult Var([Bind(Include = "NextAddForm")] ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {

                if (form_model != null)
                {
                    form_model.NewParam = (Parameter)TempData["NewParam"];
                    TempData.Keep("NewParam");
                    var Model = form_model.NextAddParameter(form_model);
                    if (!Model.NewParam.MetaData.Is_var)
                    {
                        bool result = CoreKernel.ParamRepo.Add(Model.NewParam);
                        TempData["Result"] = result;
                        return RedirectTo<AddController>(a => a.Confirm(form_model));
                    }                        
                    else
                    {
                        ViewData["NumVar"] = form_model.NextAddForm.NumVariant;
                        Model.display_for = form_model.NextAddForm.NumVariant;
                        return View(new ParametersViewModel { 
                        display_for = Model.display_for,
                        NewParam = Model.NewParam
                        });
                    }
                    
                }
                else
                    RedirectTo<AddController>(a => a.Index(form_model));
            }

            return View();
        }

        public ActionResult Confirm([Bind(Include = "VarForm")] ParametersViewModel form_model)
        {
            if(ModelState.IsValid)
            {

                form_model.NewParam = (Parameter)TempData["NewParam"];
                TempData.Keep("NewParam");
                ParametersViewModel Model = form_model.AddParameter(form_model);
                bool result = CoreKernel.ParamRepo.Add(Model.NewParam);
                TempData["Result"] = result;

                ViewData["Result"] = TempData["Result"];
                TempData.Keep("Result");
            }            
            return View();
        }
    }
}
