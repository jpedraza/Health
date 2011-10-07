using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Health.Core.API.Repository;
using Health.Site.Controllers;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Areas.Admin.Models;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models.Forms;

namespace Health.Site.Areas.Admin.Controllers
{
    public class ParametersController : CoreController
    {
        public ParametersController(IDIKernel di_kernel):base(di_kernel)
        {
        }

        #region StatrWorkOnParameters
        //
        // GET: /Admin/Parameters/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Parameters/ShowAll
        public ActionResult ShowAll()
        {
            return View(new ParametersViewModel
            {
                parameters = Get<IParameterRepository>().GetAllParam()

            });
        }

        #endregion

        #region AddingParameter

        //
        // GET: /Admin/Parameters/AddParameter
        [PRGImport, ValidationModel]
        public ActionResult AddParameter(AddFormModel form)
        {
            return View(new AddFormModel
            {
                Parameters = Get<IParameterRepository>().GetAllParam(),
                CheckBoxesParents = new List<bool>(),
                CheckBoxesChildren = new List<bool>()
            });
        }


        public ActionResult AddingParameter(AddFormModel form)
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
                        return RedirectTo<ParametersController>(a => a.AddConfirm());
                    }
                    else
                    {
                        if (form.NumValue == 0)
                        {
                            return RedirectTo<ParametersController>(a => a.AddParameter(form));
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
                            return RedirectTo<ParametersController>(a => a.Var(varForm));
                        }
                    }
                }
                else
                    throw new Exception("Отсутствует форма с данными,необходимы для создания параметра здоровья.");
            }
            else
                throw new Exception("Форма с входными данными не прошла валидацию.");
        }

        public ActionResult AddConfirm()
        {
            return View();
        }

        [PRGImport, ValidationModel]
        public ActionResult Var(VarFormModel form)
        {
            return View(form);
        }

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
                        throw new Exception("Ошибка, параметр еще не создан.");
                    form.Parameter.MetaData.Variants = form.Variants;
                    Get<IParameterRepository>().Add(form.Parameter);
                    return RedirectTo<ParametersController>(a => a.AddConfirm());
                }

                else
                {
                    throw new Exception("Ошибка. Параметр еще не создан.");
                }
            }
            else
            {
                return RedirectTo<ParametersController>(aa => aa.Var(form));
            }
        }
        #endregion

        #region EditingParameter

        public ActionResult StartEditing()
        {
            ViewData["Parameters"] = Get<IParameterRepository>().GetAllParam();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Parameter parameter = Get<IParameterRepository>().GetById(id);
            if (parameter == null)
            {
                throw new Exception(String.Format("Параметра с ID = {0} не существует", id));
            }
            if (parameter.MetaData.Is_var)
            {
                IList<Variant> variants = parameter.MetaData.Variants.ToList();
                variants.Add(new Variant());
                parameter.MetaData.Variants = variants.ToArray();
            }
            return RedirectTo<ParametersController>(a => a.EditParam(new EditingFormModel()
            {
                CheckBoxesParents = new List<bool>(),
                CheckBoxesChildren = new List<bool>(),
                NumValue =
                    !parameter.MetaData.Is_var
                        ? 0
                        : parameter.MetaData.Variants.Length,
                parameter = parameter,
                Parameters =
                    Get<IParameterRepository>().GetAllParam(),
                CheckBoxVariant = new List<bool>(),
                Variants = parameter.MetaData.Variants
            }
                                                          ));
        }

        [PRGImport, ValidationModel]
        public ActionResult EditParam(EditingFormModel form)
        {
            if (form == null || form.parameter == null)
            {
                return RedirectTo<ParametersController>(a => a.StartEditing());
            }
            else
                return View(form);
        }

        public ActionResult SaveEdit(EditingFormModel form)
        {
            if (form != null && ModelState.IsValid)
            {
                form.Parameters = Get<IParameterRepository>().GetAllParam();
                var parameter = Get<IParameterRepository>().GetById(form.parameter.Id);
                form.parameter.MetaData.Variants = parameter.MetaData.Variants;
                if (form.parameter.MetaData.Is_var)
                {
                    if (form.parameter.MetaData.Variants == null)
                    {
                        if (form.NumValue != 0)
                        {

                            return RedirectTo<ParametersController>(z => z.AddVariant(form));
                        }
                        else
                        {
                            form.parameter.MetaData.Is_var = !form.parameter.MetaData.Is_var;
                            return RedirectTo<ParametersController>(z => z.EditParam(form));
                        }
                    }
                    else
                    {
                        if (form.Variants.Last().Value != null && form.Variants.Last().Ball != null)
                        {
                            //TODO: решить проблему - если задать пустые ячейки, то они запишутся в БД.
                            form.parameter.MetaData.Variants = form.Variants;
                        }
                        else
                        {
                            IList<Variant> variants = form.Variants.ToList();
                            variants.RemoveAt(variants.Count - 1);
                            form.Variants = variants.ToArray();
                        }

                        for (var i = 0; i < form.CheckBoxVariant.Count; i++)
                        {
                            if (form.CheckBoxVariant[i])
                            {
                                IList<Variant> variants = form.Variants.ToList();
                                variants.RemoveAt(i);
                                form.Variants = variants.ToArray();
                            }
                        }

                        SaveParameter(form);
                        return RedirectTo<ParametersController>(z => z.EditConfirm());
                    }
                }
                else
                {
                    if (form.parameter.MetaData.Variants == null)
                    {
                        SaveParameter(form);
                        return RedirectTo<ParametersController>(z => z.EditConfirm());
                    }
                    else
                    {
                        form.Variants = null;
                        form.parameter.MetaData.Is_var = false;
                        SaveParameter(form);
                        return RedirectTo<ParametersController>(z => z.EditConfirm());
                    }
                }
            }
            else
            {
                throw new Exception("Ошибка, нет заполненной формы редактирования параметра.");
            }
            return View();
        }

        private void SaveParameter(EditingFormModel form)
        {
            form.parameter.MetaData.Variants = form.Variants;
            var model = new ParametersViewModel() { EditingForm = form };
            model.SaveParentsAndChildren();
            Get<IParameterRepository>().Edit(form.parameter);
        }

        [PRGImport, PRGExport]
        public ActionResult AddVariant(EditingFormModel form)
        {
            if (form == null && ModelState.IsValid == false && form.NumValue == 0)
            {
                throw new Exception("Ошибка, нет данных для добавления вариантов ответа");
            }
            TempData["parameter"] = form.parameter;
            return View(new VarFormModel()
            {
                NumVariant = form.NumValue,
                Variants = new Variant[form.NumValue],
                Parameter = form.parameter

            });
        }

        public ActionResult Addingvariant(VarFormModel form)
        {
            if (form == null)
                throw new Exception("Ошибка. Параметр еще не создан.");
            if (ModelState.IsValid)
            {
                if (form.Variants != null)
                {
                    form.Parameter = TempData["parameter"] as Parameter;
                    TempData.Keep("parameter");
                    if (form.Parameter == null)
                        throw new Exception("Ошибка, параметр еще не создан.");
                    form.Parameter.MetaData.Variants = form.Variants;
                    Get<IParameterRepository>().Edit(form.Parameter);
                    return RedirectTo<ParametersController>(a => a.EditConfirm());
                }

                else
                {
                    throw new Exception("Ошибка. Параметр еще не создан.");
                }
            }
            else
            {
                return RedirectTo<ParametersController>(aa => aa.AddVariant(new EditingFormModel()
                {
                    parameter = form.Parameter,
                    NumValue = form.NumVariant
                }));
            }
        }

        public ActionResult EditConfirm()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (Get<IParameterRepository>().DeleteParam(id))
            {
                return RedirectTo<ParametersController>(s => s.EditConfirm());
            }
            else
            {
                throw new Exception(String.Format("Ошибка. Невозможно удалить параметр здоровья с Id ={0}", id));
            }
        }
        #endregion
    }
}
