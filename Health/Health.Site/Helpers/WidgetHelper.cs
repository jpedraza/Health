using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Health.Site.Controllers;
using System.Web.Mvc.Html;

namespace Health.Site.Helpers
{
    public static class WidgetHelper
    {
        public static MvcHtmlString Widget(this HtmlHelper helper, Expression<Action<WidgetController>> action)
        {
            var info = (MethodCallExpression)action.Body;
            var name = info.Method.Name;
            return helper.Action(name, "Widget", new { area = "" });
        }
    }
}