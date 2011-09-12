using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Binders
{
    /// <summary>
    /// Binder для параметрических форм.
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
            return Activator.CreateInstance(model_type, new[] {Kernel});
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
        protected IEnumerable<Parameter> GetValueForParameter(ControllerContext controller_context,
                                                              ModelBindingContext binding_context)
        {
            NameValueCollection value_collection = controller_context.HttpContext.Request.Form;
            List<Parameter> parameters = Kernel.Get<IEnumerable<Parameter>>().ToList();
            if (value_collection.Count != 0)
            {
                int count = value_collection.Cast<object>().Count(key => key.ToString().Contains("Parameters"));
                string format = binding_context.ModelName + ".Parameters[{0}].{1}";

                for (int i = 0; i < count/2; i++)
                {
                    parameters.Add(new Parameter
                                       {
                                           Name =
                                               value_collection[
                                                   String.Format(format, i, "Name")],
                                           Value =
                                               value_collection[
                                                   String.Format(format, i, "Value")]
                                       });
                }
            }
            return parameters;
        }
    }
}