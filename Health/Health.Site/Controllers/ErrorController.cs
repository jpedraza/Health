using System.Web.Mvc;
using Health.Site.Attributes;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    /// <summary>
    /// Контроллер обрабатывает все ошибки в приложении.
    /// </summary>
    public class ErrorController : Controller
    {
        [PRGImport(ParametersHook = true)]
        public ActionResult Index([Bind(Include = "ErrorModel")] ErrorViewModel error_model)
        {
            if (error_model != null)
            {
                return View(error_model);
            }
            return View();
        }
    }
}