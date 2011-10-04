using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Health.Core.API.Repository;
using Health.Site.Controllers;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Areas.Parameters.Models;
using Health.Core.Entities.POCO;
using Health.Site.Models.Metadata;
using Health.Data.Repository.Fake;
using Health.Site.Areas.Parameters.Models.Forms;

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
        //TODO: Перед формами написать названия процессов, показывающих, что делает эта форма
        /// <summary>
        /// Отображение формы начала создания нового параметра
        /// </summary>
        [PRGImport, ValidationModel]
        public ActionResult Index(AddFormModel form)
        {
            return View(new AddFormModel { 
            Parameters = Get<IParameterRepository>().GetAllParam(),
            CheckBoxesParents = new List<bool>(),
            CheckBoxesChildren = new List<bool>()
            });
        }

        
        /// <summary>
        /// Отображение формы для продолжения создания нового параметра
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost, PRGExport, ValidationModel]
        public ActionResult Add(AddFormModel form)
        {
            ParametersViewModel viewModel = new ParametersViewModel();
            if (ModelState.IsValid)
            {
                if (form != null)
                {
                    viewModel.AddForm = form;
                    viewModel.AddForm.Parameters = Get<IParameterRepository>().GetAllParam();
                    viewModel.SetPropertiesAndMetadata();
                    if (!form.parameter.MetaData.Is_var)
                    {
                        var flag = Get<IParameterRepository>().Add(form.parameter);
                        return RedirectTo<AddController>(a => a.Confirm());
                    }
                    else
                    {
                        if (form.NumValue == 0)
                        {
                            return RedirectTo<AddController>(a => a.Index(form));
                        }
                        else
                        {
                            TempData["newParameter"] = form.parameter;
                            var varForm = new VarFormModel()
                            {
                                NumVariant = form.NumValue,
                                Variants = new Variant[form.NumValue],
                                Parameter = form.parameter
                            };
                            return RedirectTo<AddController>(a => a.Var(varForm));
                        }
                    }
                }
                else
                    throw new Exception("Отсутствует форма с данными,необходимы для создания параметра здоровья.");
            }
            else
                throw new Exception("Форма с входными данными не прошла валидацию.");
        }

        /// <summary>
        /// Генерирует форму, для заполнения вариантов ответа на параметр здоровья
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [PRGImport, ValidationModel]//тоже
        public ActionResult Var(VarFormModel form)
        {
            return View(form);
        }

        /// <summary>
        /// Проверяет правильность заполнения вариантов ответа, и добавляет их к существующему параметру.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [PRGExport]
        public ActionResult AddVar(VarFormModel form)
        {
            if (form == null)
                throw new Exception("Ошибка. Параметр еще не создан.");
            if (ModelState.IsValid)
            {
                if (form.Variants != null)
                {
                    form.Parameter = TempData["newParameter"] as Parameter;
                    if (form.Parameter == null)
                        throw new AbandonedMutexException("Ошибка, параметр еще не создан.");
                    form.Parameter.MetaData.Variants = form.Variants;
                    Get<IParameterRepository>().Add(form.Parameter);
                    return RedirectTo<AddController>(a => a.Confirm());
                }

                else
                {
                    throw new Exception("Ошибка. Параметр еще не создан.");
                }
            }
            else
            {
                return RedirectTo<AddController>(aa => aa.Var(form));
            }
        }

        public ActionResult Confirm()
        {
            return View();
        }
        /*
        /// <summary>
        /// Отображение формы 
        /// </summary>
        /// <param name="form_model"></param>
        /// <returns></returns>
        [PRGExport, PRGImport]
        public ActionResult Var( ParametersViewModel form_model)
        {
            //if (ModelState.IsValid)
            //{
            //    form_model.NewParam = TempData["NewParam"] as Parameter;
            //    TempData.Keep("NewParam");
            //    if (form_model.NextAddForm != null && form_model.NewParam != null)
            //    {
            //        form_model.NextAddParameter();
            //        if (!form_model.NewParam.MetaData.Is_var)
            //        {
            //            return RedirectTo<AddController>(a => a.Confirm(form_model));
            //        }                        
            //        else
            //        {
            //            form_model.display_for = form_model.NextAddForm.NumVariant;
            //            TempData["NewParam"] = form_model.NewParam;

            //            return View(form_model);
            //        }
                    
            //    }
            //    else
            //        return RedirectTo<AddController>(a => a.Index());
            //}

            return View();
        }

        public ActionResult Confirm(ParametersViewModel form_model)
        {
            //if (ModelState.IsValid)
            //{

            //    form_model.NewParam = TempData["NewParam"] as Parameter;
            //    TempData.Keep("NewParam");
            //    if(form_model.NewParam == null)
            //        return RedirectTo<AddController>(a => a.Index());
            //    if (form_model.VarForm != null)
            //        form_model.AddVariants();
            //    bool result = Get<IParameterRepository>().Add(form_model.NewParam);
            //    TempData["Result"] = result;
            //    ViewData["Result"] = TempData["Result"];
            //    TempData.Keep("Result");
            //    return View();
            //}
            //else
            //{ 
            //    return RedirectTo<AddController>(a => a.Var(form_model));
            //}
           
        }*/
    }
}