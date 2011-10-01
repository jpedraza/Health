using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.TypeProvider;

namespace Health.Site.Filters
{
    public class ValidationModelAttributeFilter : IActionFilter
    {
        private readonly Type _for;
        private readonly Type _use;
        private readonly IDIKernel _diKernel;
        private readonly string _alias;

        public ValidationModelAttributeFilter(IDIKernel diKernel, Type @for, Type use, string alias)
        {
            _for = @for;
            _use = use;
            _diKernel = diKernel;
            _alias = alias;
        }

        #region Implementation of IActionFilter

        /// <summary>
        /// Вызывается до выполнения метода действия.
        /// </summary>
        /// <param name="filterContext">Контекст фильтра.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object model;
            if (!filterContext.ActionParameters.TryGetValue(_alias, out model))
            {
                throw new Exception("Модель в параметрах метода не найдена. Проверьте что имя параметра 'form' или укажите псевдоним Alias.");
            }
            FindModelMetadataForType(model.GetType());
            var adapter = ModelValidatorProviders.Providers[0];
            ModelMetadata modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType()); 
            IEnumerable<ModelValidator> validators = adapter.GetValidators(modelMetadata,
                                  filterContext.Controller.ControllerContext);
            filterContext.Controller.ViewData.ModelState.Clear();
            foreach (ModelValidator validator in validators)
            {
                IEnumerable<ModelValidationResult> result = validator.Validate(model);
                foreach (ModelValidationResult modelValidationResult in result)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError(modelValidationResult.MemberName, modelValidationResult.Message);
                }
            }
            IModelBinder binder = ModelBinders.Binders.GetBinder(model.GetType());
            var bindingContext = new ModelBindingContext
                                     {
                                         ModelMetadata = modelMetadata,
                                         ModelState = filterContext.Controller.ViewData.ModelState,
                                         ValueProvider = filterContext.Controller.ValueProvider
                                     };
            binder.BindModel(filterContext.Controller.ControllerContext, bindingContext);
            filterContext.Controller.ViewData.Model = model;
        }

        private void FindModelMetadataForType(Type modelType)
        {
            PropertyInfo[] propertyInfos = modelType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.Namespace != typeof(int).Namespace)
                {
                    if (propertyInfo.PropertyType == _for)
                    {
                        FindModelMetadataForType(_use);
                        continue;
                    }
                    var attribute =
                        propertyInfo.GetCustomAttributes(typeof (ClassMetadataAttribute), true).FirstOrDefault() as
                        ClassMetadataAttribute;
                    if (attribute != null)
                    {
                        _diKernel.Get<DynamicMetadataRepository>().Bind(propertyInfo.PropertyType,
                                                                        attribute.MetadataType);
                        FindModelMetadataForType(attribute.MetadataType);
                    }
                }
            }
        }

        /// <summary>
        /// Вызывается после выполнения метода действия.
        /// </summary>
        /// <param name="filterContext">Контекст фильтра.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext) { }

        #endregion
    }
}