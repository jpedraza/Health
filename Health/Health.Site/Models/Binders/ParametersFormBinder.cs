using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Site.Areas.Account.Models.Forms;

namespace Health.Site.Models.Binders
{
    /// <summary>
    /// Binder для форм с набором параметров.
    /// </summary>
    public class ParametersFormBinder : DefaultModelBinder
    {
        public ParametersFormBinder(IDIKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// DI ядро.
        /// </summary>
        protected IDIKernel Kernel { get; set; }

        /// <summary>
        /// Создание модели.
        /// </summary>
        /// <param name="controller_context">Контекст контроллера.</param>
        /// <param name="binding_context">Контекст привязки.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Экземпляр модели.</returns>
        protected override object CreateModel(ControllerContext controller_context, ModelBindingContext binding_context,
                                              Type model_type)
        {
            return Activator.CreateInstance(model_type, new[] { Kernel });
        }

        /// <summary>
        /// Привязка свойств модели.
        /// </summary>
        /// <param name="controller_context">Контекст контроллера.</param>
        /// <param name="binding_context">Контекст привязки.</param>
        /// <param name="property_descriptor">Дескриптор свойства.</param>
        protected override void BindProperty(ControllerContext controller_context, ModelBindingContext binding_context,
                                             PropertyDescriptor property_descriptor)
        {
            SetProperty(controller_context, binding_context, property_descriptor,
                        GetValueForParameter(controller_context, binding_context));
        }

        /// <summary>
        /// Получение значений параметров.
        /// </summary>
        /// <param name="controller_context">Контекст контроллера.</param>
        /// <param name="binding_context">Контекст привязки.</param>
        /// <returns>Перечисление параметров.</returns>
        protected IEnumerable<IParameter> GetValueForParameter(ControllerContext controller_context,
                                                               ModelBindingContext binding_context)
        {
            NameValueCollection value_collection = controller_context.HttpContext.Request.Form;

            var parameters = Kernel.Get<IEnumerable<IParameter>>();
            List<IParameter> list_parameters = parameters.ToList();

            int count = 0;
            foreach (object key in value_collection)
            {
                if (key.ToString().Contains("Parameters"))
                {
                    count++;
                }
            }
            const string format = "InterviewForm.Parameters[{0}].{1}";
            for (int i = 0; i < count/2; i++)
            {
                var parameter = Kernel.Get<IParameter>();
                parameter.Name = value_collection[String.Format(format, i, "Name")];
                parameter.Value = value_collection[String.Format(format, i, "Value")];
                list_parameters.Add(parameter);
            }

            return list_parameters;
        }
    }
}