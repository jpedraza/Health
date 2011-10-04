using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Factory;
namespace Health.Site.Helpers
{
    public static class RenderParameters
    {
        private static RenderingFactory Factory { get; set; }

        public static HtmlString GetLayoutForParameterForPatient(this HtmlHelper helper, Parameter parameter, string name_prefix)
        {
            if (Factory == null)
            {
                Factory = new RenderingFactory(parameter, name_prefix);
            }
            return Factory.GetPatientParameterLayout();
        }

        public static HtmlString NewParameterDraw(this HtmlHelper helper, Parameter parameter, string name_prefix)
        {
            if (Factory == null)
            {
                Factory = new RenderingFactory(parameter, name_prefix);
            }
            return Factory.GetAddParameterLayout(parameter);
        }
    }
}