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
    /// <summary>
    /// Хелперы для виджетов.
    /// </summary>
    public static class WidgetHelper
    {
        /// <summary>
        /// Отрисовка виджета.
        /// </summary>
        /// <param name="helper">Объект расширения.</param>
        /// <param name="action">Виджет.</param>
        /// <returns>Виджет.</returns>
        public static MvcHtmlString Widget(this HtmlHelper helper, Expression<Action<WidgetController>> action)
        {
            var info = (MethodCallExpression)action.Body;
            var name = info.Method.Name;
            return helper.Action(name, "Widget", new { area = "" });
        }
    }
}