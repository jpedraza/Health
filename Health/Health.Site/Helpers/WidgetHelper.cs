using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Health.Site.Controllers;

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
            var info = (MethodCallExpression) action.Body;
            string name = info.Method.Name;
            return helper.Action(name, "Widget", new {area = ""});
        }
    }
}