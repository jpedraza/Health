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
        // GET: /Parameters/Editing/

        public ActionResult Index()
        {
            ViewData["Parameters"] = CoreKernel.ParamRepo.GetAllParam();
            return View();
        }

        //
        // GET: /Parameters/Editing/Edit
        //TODO: сделать страницу и сообщения пользователю об ошибке; а также страницу подтверждения.

        public ActionResult Edit(int parameter_id)
        {
            Parameter parameter = CoreKernel.ParamRepo.GetById(parameter_id);
            if (parameter == null)
            {
                var str = "Параметра с ID = " + parameter_id.ToString() + " не существует";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
            TempData["EditParameter"] = parameter;
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

        public ActionResult ContinueEdit()
        {
            Parameter parameter = TempData["EditParameter"] as Parameter;
            if (parameter == null)
            {
                var str = "Ошибка, нет данных (параметра) для продолжения редактирования.";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
            // Следующая операция необходима для обновления параметра в модели.
            // А предыдущая нужна для получения ID обновляемого параметра
            parameter = CoreKernel.ParamRepo.GetById(parameter.Id);
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
                var str = "Ошибка, нет данных (параметра) для продолжения редактирования.";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
        }
        //TODO: В методах модели пофиксить алгоритм, вместо return this, некоторые методы сделать через void.

        //TODO: Сделать так, чтоб при нажатии кнопки назад после confirm вернуться было нельзя.
        public ActionResult Save([Bind(Include = "EditingForm")] ParametersViewModel form_model)
        {
            form_model.EditParam = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (form_model.EditParam != null && form_model.EditingForm != null)
            {
                form_model.SaveEditngParameter();
                bool Result = CoreKernel.ParamRepo.Edit(form_model.EditParam);
                TempData["Result"] = Result.ToString();
                return RedirectTo<EditingController>(a => a.ConfirmSave());
            }
            else
            {
                var str = "Ошибка, нет данных для обновления параметра.";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
        }

        public ActionResult AddVariant()
        {
            Parameter parameter = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (parameter != null)
            {
                return View(new ParametersViewModel { EditParam = parameter});
            }
            else
            {
                var str = "Ошибка, нет данных (параметра) для добавления варианта ответа.";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
        }

        public ActionResult SaveAddVariant([Bind(Include = "VarForm")] ParametersViewModel form_model)
        {
            form_model.EditParam = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (form_model.EditParam != null & form_model.VarForm.variants != null)
            {
                form_model.AddVariant();
                CoreKernel.ParamRepo.Edit(form_model.EditParam);
                return RedirectTo<EditingController>(s => s.ContinueEdit());
            }
            else
            {
                var str = "Ошибка, нет данных (параметра) для добавления варианта ответа.";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
        }

        public ActionResult Deletevariant(int variant_id)
        {
            Parameter parameter = TempData["EditParameter"] as Parameter;
            TempData.Keep("EditParameter");
            if (parameter != null)
            {
                ParametersViewModel.DeleteVariant(variant_id, parameter);
                TempData["EditParameter"] = parameter;
                CoreKernel.ParamRepo.Edit(parameter);
                return RedirectTo<EditingController>(s => s.ContinueEdit());
            }
            else
            {
                var str = "Ошибка, нет данных (параметра) для удаления варианта ответа.";
                TempData["Error"] = str;
                return RedirectTo<EditingController>(a => a.Error());
            }
        }

        public ActionResult ConfirmSave()
        {
            ViewData["Result"] = TempData["Result"];
            return View();
        }

        public ActionResult Error()
        {
            ViewData["Message"] = TempData["Error"];
            return View();
        }

        public ActionResult Delete(int parameter_id)
        {
            TempData["Result"] = CoreKernel.ParamRepo.DeleteParam(parameter_id);
            return RedirectTo<EditingController>(g => g.ConfirmSave());
        }
    }
}
