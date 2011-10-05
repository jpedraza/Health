using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Health.Core.API.Repository;
using Health.Site.Controllers;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Areas.Parameters.Models;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Parameters;
using Health.Site.Areas.Parameters.Models.Forms;

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
        
        
        public ActionResult Edit(int parameter_id)
        {
            Parameter parameter = Get<IParameterRepository>().GetById(parameter_id);
            if (parameter == null)
            {
                throw new Exception(String.Format("Параметра с ID = {0} не существует", parameter_id));
            }
            if(parameter.MetaData.Is_var)
            {
                IList<Variant> variants = parameter.MetaData.Variants.ToList();
                variants.Add(new Variant());
                parameter.MetaData.Variants = variants.ToArray();
            }
            return RedirectTo<EditingController>(a => a.EditParam(new EditingFormModel()
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
                return RedirectTo<EditingController>(a => a.Index());
            }
            else
                return View(form);
        }

        public ActionResult SaveEdit(EditingFormModel form)
        {
            if(form!= null && ModelState.IsValid)
            {
                form.Parameters = Get<IParameterRepository>().GetAllParam();
                var parameter = Get<IParameterRepository>().GetById(form.parameter.Id);
                form.parameter.MetaData.Variants = parameter.MetaData.Variants;
                if(form.parameter.MetaData.Is_var)
                {
                    if(form.parameter.MetaData.Variants == null)
                    {
                        if (form.NumValue != 0)
                        {
                            
                            return RedirectTo<EditingController>(z => z.AddVariant(form));
                        }
                        else
                        {   
                            form.parameter.MetaData.Is_var = !form.parameter.MetaData.Is_var;
                            return RedirectTo<EditingController>(z => z.EditParam(form));
                        }
                    }
                    else
                    {
                        if(form.Variants.Last().Value != null && form.Variants.Last().Ball != null)
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
                            if(form.CheckBoxVariant[i])
                            {
                                IList<Variant> variants = form.Variants.ToList();
                                variants.RemoveAt(i);
                                form.Variants = variants.ToArray();
                            }
                        }

                        SaveParameter(form);
                        return RedirectTo<EditingController>(z => z.Confirm());
                    }
                }
                else
                {
                    if(form.parameter.MetaData.Variants == null)
                    {
                        SaveParameter(form);
                        return RedirectTo<EditingController>(z => z.Confirm());
                    }
                    else
                    {
                        form.Variants = null;
                        form.parameter.MetaData.Is_var = false;
                        SaveParameter(form);
                        return RedirectTo<EditingController>(z => z.Confirm());
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
            var model = new ParametersViewModel() {EditingForm = form};
            model.SaveParentsAndChildren();
            Get<IParameterRepository>().Edit(form.parameter);
        }

        [PRGImport, PRGExport]
        public ActionResult AddVariant(EditingFormModel form)
        {
            if(form == null && ModelState.IsValid == false && form.NumValue == 0)
            {
                throw new Exception("Ошибка, нет данных для добавления вариантов ответа");
            }
            TempData["parameter"] = form.parameter;
            return View(new VarFormModel()
                            {
                                NumVariant = form.NumValue,
                                Variants = new Variant[form.NumValue],
                                Parameter =  form.parameter

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
                    return RedirectTo<EditingController>(a => a.Confirm());
                }

                else
                {
                    throw new Exception("Ошибка. Параметр еще не создан.");
                }
            }
            else
            {
                return RedirectTo<EditingController>(aa => aa.AddVariant(new EditingFormModel()
                                                                             {
                                                                                 parameter = form.Parameter,
                                                                                 NumValue = form.NumVariant
                                                                             }));
            }
        }

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult Delete(int parameter_id)
        {
            if(Get<IParameterRepository>().DeleteParam(parameter_id))
            {
                return RedirectTo<EditingController>(s => s.Confirm());
            }
            else
            {
                throw  new Exception(String.Format("Ошибка. Невозможно удалить параметр здоровья с Id ={0}", parameter_id));
            }
        }

        /*
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
         */
    }
}
