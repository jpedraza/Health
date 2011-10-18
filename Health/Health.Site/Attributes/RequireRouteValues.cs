using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Health.Site.Attributes
{
    public class RequiredParametersInRouteValuesAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            bool contains = false;
            ParameterInfo[] parameters = methodInfo.GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                contains = controllerContext.RequestContext.RouteData.Values.ContainsKey(parameter.Name);
                if (!contains) break;
            }
            return contains;
        }
    } 

    public class FormatActionName : IRouteHandler
    {
        #region Implementation of IRouteHandler

        /// <summary>
        /// Предоставляет объект, обрабатывающий запрос.
        /// </summary>
        /// <returns>
        /// Объект, обрабатывающий запрос.
        /// </returns>
        /// <param name="requestContext">Объект, инкапсулирующий сведения о запросе.</param>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var route = requestContext.RouteData.Route as Route;
            if (route != null)
            {
                string pre = requestContext.RouteData.Values["pre_action"].ToString();
                string post = requestContext.RouteData.Values["post_action"].ToString();
                string format = route.Defaults["format"].ToString();
                if (requestContext.RouteData.Values.ContainsKey("action"))
                {
                    requestContext.RouteData.Values["action"] = String.Format(format, pre, post);
                }
                else
                {
                    requestContext.RouteData.Values.Add("action", String.Format(format, pre, post));
                }
            }
            return new MvcHandler(requestContext);
        }

        #endregion
    }
}