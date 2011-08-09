using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Attributes
{
    public abstract class PRGModelState : ActionFilterAttribute
    {
        protected static readonly string Key = typeof (PRGModelState).FullName;
    }

    public class PRGExport : PRGModelState
    {
        public override void OnActionExecuted(ActionExecutedContext filter_context)
        {
            if (!filter_context.Controller.ViewData.ModelState.IsValid)
            {
                if ((filter_context.Result is RedirectResult) || (filter_context.Result is RedirectToRouteResult))
                {
                    filter_context.Controller.TempData[Key] = filter_context.Controller.ViewData.ModelState;
                }
            }
            base.OnActionExecuted(filter_context);
        }

        public override void OnActionExecuting(ActionExecutingContext filter_context)
        {
            base.OnActionExecuting(filter_context);
        }
    }

    public class PRGImport : PRGModelState
    {
        public override void OnActionExecuted(ActionExecutedContext filter_context)
        {
            var model_state = filter_context.Controller.TempData[Key] as ModelStateDictionary;

            if (model_state != null)
            {
                if (filter_context.Result is ViewResult)
                {
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