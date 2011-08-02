using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.API.Entities;

namespace Health.Site.Helpers
{
    #region IParameterDraw
    public interface IParameterDraw
    {
        MvcHtmlString Layout(IParameter parameter);
    }
    #endregion

    #region ParametersHelper
    public static class ParametersHelper
    {
        public static MvcHtmlString ParametersFactory(this HtmlHelper helper, IEnumerable<IParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                // Тут вызываем нужный отрисовщик
            }
            return MvcHtmlString.Create("");
        }
    }
    #endregion

    #region ParameterDraw
    public class ParameterTextBoxDraw : IParameterDraw
    {
        public MvcHtmlString Layout(IParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}