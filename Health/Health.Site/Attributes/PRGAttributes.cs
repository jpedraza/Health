using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Attributes
{
    /// <summary>
    /// Абстрактный класс для работы с PRG - pattern.
    /// </summary>
    public abstract class PRGModelState : ActionFilterAttribute
    {
        /// <summary>
        /// Идентификатор состояния модели в хранилище.
        /// </summary>
        protected static readonly string Key = typeof (PRGModelState).FullName;
    }

    /// <summary>
    /// Атрибут для автоматического экспорта состояния модели в хранилище.
    /// </summary>
    public class PRGExport : PRGModelState
    {
        public override void OnActionExecuted(ActionExecutedContext filter_context)
        {
            if (!filter_context.Controller.ViewData.ModelState.IsValid)
            {
                if ((filter_context.Result is RedirectResult) || (filter_context.Result is RedirectToRouteResult))
                {
                    // Сохраняем состояние модели в хранилище.
                    filter_context.Controller.TempData[Key] = filter_context.Controller.ViewData.ModelState;
                }
            }
            base.OnActionExecuted(filter_context);
        }
    }

    /// <summary>
    /// Атрибут для автоматического импорта модели из хранилища.
    /// </summary>
    public class PRGImport : PRGModelState
    {
        public override void OnActionExecuted(ActionExecutedContext filter_context)
        {
            var model_state = filter_context.Controller.TempData[Key] as ModelStateDictionary;

            if (model_state != null)
            {
                if (filter_context.Result is ViewResult)
                {
                    // Производим слияние состояний модели.
                    filter_context.Controller.ViewData.ModelState.Merge(model_state);
                }
                else
                {
                    filter_context.Controller.TempData.Remove(Key);
                }
            }
            base.OnActionExecuted(filter_context);
        }
    }
}