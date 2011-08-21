using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Microsoft.Web.Mvc;

namespace Health.Site.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult RedirectTo<T>(this Controller controller, Expression<Action<T>> action)
            where T : Controller
        {
            IList<PRGParameter> prg_parameters = PRGModelState.GetExportModel(action);
            controller.TempData[PRGModelState.PRGParametersKey] = prg_parameters;
            return controller.RedirectToAction(action);
        }
    }
}