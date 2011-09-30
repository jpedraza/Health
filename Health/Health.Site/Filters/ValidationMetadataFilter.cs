using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.Virtual;
using Health.Core.TypeProvider;
using Health.Site.Areas.Schedules.Controllers;
using Health.Site.Models.Metadata;
using Health.Site.Models.Providers;
using Health.Site.Repository;

namespace Health.Site.Filters
{
    public class ValidationMetadataFilter : IActionFilter
    {
        private readonly Type _for;
        private readonly Type _use;
        private readonly IDIKernel _diKernel;

        public ValidationMetadataFilter(IDIKernel diKernel, Type @for, Type use)
        {
            _for = @for;
            _use = use;
            _diKernel = diKernel;
        }

        #region Implementation of IActionFilter

        /// <summary>
        /// Вызывается до выполнения метода действия.
        /// </summary>
        /// <param name="filterContext">Контекст фильтра.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _diKernel.Get<DynamicMetadataRepository>().Bind(_for, _use);
            object model = filterContext.ActionParameters["form"];
            var adapter = new ModelValidatorProviderAdapter(_diKernel);
            IEnumerable<ModelValidator> validators = adapter.GetValidators(filterContext.Controller.ViewData.ModelMetadata,
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
            filterContext.Controller.ViewData.Model = model;
            IModelBinder binder = ModelBinders.Binders.GetBinder(model.GetType());

            var bindingContext = new ModelBindingContext
                                                     {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType()),
                ModelName = "",
                ModelState = filterContext.Controller.ViewData.ModelState,
                PropertyFilter = null,
                ValueProvider = filterContext.Controller.ValueProvider
            };
            binder.BindModel(filterContext.Controller.ControllerContext, bindingContext);
        }

        /// <summary>
        /// Вызывается после выполнения метода действия.
        /// </summary>
        /// <param name="filterContext">Контекст фильтра.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext) { }

        #endregion
    }
}