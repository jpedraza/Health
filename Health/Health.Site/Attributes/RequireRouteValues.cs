using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Attributes
{
    public class RequiredParametersInRouteValuesAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controller_context, MethodInfo method_info)
        {
            bool contains = false;
            ParameterInfo[] parameters = method_info.GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                contains = controller_context.RequestContext.RouteData.Values.ContainsKey(parameter.Name);
                if (!contains) break;
            }
            return contains;
        }
    } 
}