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
using Health.Site.Models.Metadata;
using Health.Data.Repository.Fake;

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
        //TODO: Перед формами написать названия процессов, показывающих, что делает эта форма
        /// <summary>
        /// Отображение формы начала создания нового параметра
        /// </summary>
        [PRGImport]
        public ActionResult Index()
        {
            ClassMetadataBinder<Parameter, ParameterMetadata>();
            return View();
        }

        /// <summary>
        /// Отображение формы для продолжения создания нового параметра
        /// </summary>
        /// <param name="form_model"></param>
        /// <returns></returns>
        [PRGImport, PRGExport]
        public ActionResult NextAdd(ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                if (form_model != null && form_model.StartAddForm != null)
                {
                    form_model.StartAddParameter(DIKernel.Get<ParametersFakeRepository>());
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
        [PRGExport, PRGImport]
        public ActionResult Var( ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {
                form_model.NewParam = TempData["NewParam"] as Parameter;
                TempData.Keep("NewParam");
                if (form_model.NextAddForm != null && form_model.NewParam != null)
                {
                    form_model.NextAddParameter();
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

        public ActionResult Confirm(ParametersViewModel form_model)
        {
            if (ModelState.IsValid)
            {

                form_model.NewParam = TempData["NewParam"] as Parameter;
                TempData.Keep("NewParam");
                if(form_model.NewParam == null)
                    return RedirectTo<AddController>(a => a.Index());
                if (form_model.VarForm != null)
                    form_model.AddVariants();
                bool result = CoreKernel.ParamRepo.Add(form_model.NewParam);
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
