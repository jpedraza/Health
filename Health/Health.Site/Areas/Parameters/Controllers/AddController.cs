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
        //TODO: Добавить разметку на первой форме (пока непринципиально)
        //TODO: Сделать специальную страницу для ошибок
        //TODO: Переименовать названия контроллеров и записи типа <summary>
        /// <summary>
        /// Отображение формы начала создания нового параметра
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отображение формы для продолжения создания нового параметра
        /// </summary>
        /// <param name="form_model"></param>
        /// <returns></returns>
        public ActionResult NextAdd([Bind(Include = "StartAddForm")] ParametersViewModel form_model)
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
                    return RedirectTo<AddController>(a => a.Index());
            }

            return View();
        }
        /// <summary>
        /// Отображение формы 
        /// </summary>
        /// <param name="form_model"></param>
        /// <returns></returns>
        public ActionResult Var([Bind(Include = "NextAddForm")] ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                form_model.NewParam = (Parameter)TempData["NewParam"];
                TempData.Keep("NewParam");
                if (form_model.NextAddForm != null&&ParametersViewModel.IsCorrectStage2(form_model.NewParam))
                {
                    form_model.NextAddParameter(form_model);
                    if (!form_model.NewParam.MetaData.Is_var)
                    {
                        return RedirectTo<AddController>(a => a.Confirm(form_model));
                    }                        
                    else
                    {
                        form_model.display_for = form_model.NextAddForm.NumVariant;
                        TempData["NewParam"] = form_model.NewParam;
                        return View(form_model);
                    }
                    
                }
                else
                    return RedirectTo<AddController>(a => a.Index());
            }

            return View();
        }

        public ActionResult Confirm([Bind(Include = "VarForm")] ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {

                form_model.NewParam = (Parameter)TempData["NewParam"];
                ParametersViewModel Model = form_model.AddParameter(form_model);
                bool result = CoreKernel.ParamRepo.Add(Model.NewParam);
                TempData["Result"] = result;
                ViewData["Result"] = TempData["Result"];
                TempData.Keep("Result");
                return View();
            }
            else
            { 
                return RedirectTo<AddController>(a => a.Var(form_model));
            }
           
        }
    }
}
