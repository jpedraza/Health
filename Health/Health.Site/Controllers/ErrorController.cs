using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    /// <summary>
    /// Контроллер обрабатывает все ошибки в приложении.
    /// </summary>
    public class ErrorController : CoreController
    {
        public ErrorController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        [PRGImport(ParametersHook = true)]
        public ActionResult Index([Bind(Include = "ErrorModel")] ErrorViewModel error_model)
        {
            if (error_model != null && error_model.ErrorModel != null)
            {
                return View(error_model);
            }
            return RedirectTo<HomeController>(a => a.Index());
        }
    }
}