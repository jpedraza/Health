using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    /// <summary>
    /// Контроллер обрабатывает все ошибки в приложении.
    /// </summary>
    public class ErrorController : Controller
    {
        public ActionResult Index([Bind(Include = "ErrorModel")]ErrorViewModel form_model)
        {
            return View(form_model);
        }
    }
}
