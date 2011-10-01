using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API.Repository;
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
        // GET: /Parameters/Editing/

        public ActionResult Index()
        {
            ViewData["Parameters"] = Get<IParameterRepository>().GetAllParam();
            return View();
        }

        //
        // GET: /Parameters/Editing/Edit
        //TODO: сделать страницу и сообщения пользователю об ошибке; а также страницу подтверждения.

        [PRGImport]
        public ActionResult Edit(int parameter_id)
        {
            Parameter parameter = Get<IParameterRepository>().GetById(parameter_id);
            if (parameter == null)
            {
                throw new Exception(String.Format("Параметра с ID = {0} не существует", parameter_id));
            }
            TempData["EditParameter"] = parameter;
            {

                return View(new ParametersViewModel
                {
                    EditParam = parameter,
                    EditingForm = new Models.Forms.EditingFormModel
                    {
                        Name = parameter.Name,
                        Value = parameter.Value.ToString(),
                        DefaultValue = parameter.DefaultValue.ToString(),
                        Age = parameter.MetaData.Age.ToString(),
                        Id_cat = parameter.MetaData.Id_cat,
                        variants = parameter.MetaData.Variants
                    }
                });
            }
        }

        public ActionResult ContinueEdit()
        {
            var parameter = TempData["EditParameter"] as Parameter;
            if (parameter == null)
            {
                throw new Exception("Ошибка, нет данных (параметра) для продолжения редактирования.");
            }
            // Следующая операция необходима для обновления параметра в модели.
            // А предыдущая нужна для получения ID обновляемого параметра
            parameter = Get<IParameterRepository>().GetById(parameter.Id);
            TempData["EditParameter"] = parameter;
            if (parameter != null)
            {
                return View(new ParametersViewModel
                {
                    EditParam = parameter,
                    EditingForm = new Models.Forms.EditingFormModel
                    {
                        Name = parameter.Name,
                        Value = parameter.Value.ToString(),
                        DefaultValue = parameter.DefaultValue.ToString(),
                        Age = parameter.MetaData.Age.ToString(),
                        Id_cat = parameter.MetaData.Id_cat,
                        variants = parameter.MetaData.Variants
                    }
                });
            }
            else
            {
                throw new Exception("Ошибка, нет данных (параметра) для продолжения редактирования.");
            }
        }

        //TODO: Сделать так, чтоб при нажатии кнопки назад после confirm вернуться было нельзя.
        public ActionResult Save([Bind(Include = "EditingForm")] ParametersViewModel form_model)
        {
            form_model.EditParam = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (form_model.EditParam != null && form_model.EditingForm != null)
            {
                form_model.SaveEditngParameter();
                bool Result = Get<IParameterRepository>().Edit(form_model.EditParam);
                TempData["Result"] = Result.ToString();
                return RedirectTo<EditingController>(a => a.ConfirmSave());
            }
            else
            {               
                throw new Exception("Ошибка, нет данных для обновления параметра.");
            }
        }

        public ActionResult AddVariant(ParametersViewModel form_model)
        {
            var parameter = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");            
            if (parameter != null)
            {
                if (form_model.EditingForm != null)
                {
                    parameter.DefaultValue = form_model.EditingForm.DefaultValue;
                    parameter.MetaData.Id_cat = form_model.EditingForm.Id_cat;
                    parameter.Name = form_model.EditingForm.Name;
                    parameter.Value = form_model.EditingForm.Value;
                    TempData["EditParameter"] = parameter;
                }
                //else
                //В этом случае не надо выполнять никаких действий, а продолжить генерировать страницу
                return View(new ParametersViewModel { EditParam = parameter});
            }
            else
            {                
                throw new Exception("Ошибка, нет данных (параметра) для добавления варианта ответа.");
            }
        }

        [PRGExport]
        public ActionResult SaveAddVariant(ParametersViewModel form_model)
        {
            form_model.EditParam = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (form_model.EditParam != null && form_model.VarForm.variants != null)
            {
                form_model.AddVariant();
                Get<IParameterRepository>().Edit(form_model.EditParam);
                return RedirectTo<EditingController>(s => s.ContinueEdit());
            }
            else
            {
                throw new Exception("Ошибка, нет данных (параметра) для добавления варианта ответа.");
            }
        }

        public ActionResult Deletevariant(int variant_id)
        {
            var parameter = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (parameter != null)
            {
                ParametersViewModel.DeleteVariant(variant_id, parameter);
                TempData["EditParameter"] = parameter;
                Get<IParameterRepository>().Edit(parameter);
                return RedirectTo<EditingController>(s => s.ContinueEdit());
            }
            else
            {
                throw new Exception("Ошибка, нет данных (параметра) для удаления варианта ответа.");
            }
        }

        public ActionResult ConfirmSave()
        {
            ViewData["Result"] = TempData["Result"];
            return View();
        }

        public ActionResult Delete(int parameter_id)
        {
            TempData["Result"] = Get<IParameterRepository>().DeleteParam(parameter_id);
            return RedirectTo<EditingController>(g => g.ConfirmSave());
        }
    }
}
