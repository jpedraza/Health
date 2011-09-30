using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Factory;
namespace Health.Site.Helpers
{
    public static class RenderParameters
    {
        public static HtmlString GetLayoutForParameter(this HtmlHelper helper, Parameter parameter, string name_prefix)
        {
            var factory = new RenderingFactory(parameter, name_prefix);
            return factory.GetLayout();
        }
    }
}