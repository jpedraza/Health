using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Health.Core.API;
using Health.Site.App_Start;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Health.Site.Models;
using Ninject;

namespace Health.Site
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Регистрация глобальных фильтров.
        /// </summary>
        /// <param name="filters">Коллекция фильтров.</param>
        protected static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Регистрация роутов.
        /// </summary>
        /// <param name="routes">Коллекция роутов.</param>
        protected static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Widget/{action}");

            routes.MapRoute(
                "Appointment_DoctorSchedule",
                "Appointment/{pre_action}/{post_action}/{doctorId}/{year}/{month}/{day}",
                new { controller = "Appointment", format = "{0}/{1}" },
                new[] { "Health.Site.Controllers" }
                ).RouteHandler = new FormatActionName();

            routes.MapRoute(
                "Default_Appointment_DoctorSchedule",
                "Appointment/{action}/{doctorId}/{year}/{month}/{day}",
                new {controller = "Appointment"},
                new[] {"Health.Site.Controllers"}
                );

            routes.MapRoute(
                "Default", // Имя маршрута
                "{controller}/{action}/{id}", // URL-адрес с параметрами
                new{controller = "Home", action = "Index", id = UrlParameter.Optional}, // Параметры по умолчанию
                new[] {"Health.Site.Controllers"}
                );
        }

        /// <summary>
        /// Обработка ошибок в приложении.
        /// </summary>
        protected void Application_Error()
        {
            Response.Clear();
            Response.StatusCode = 503;
            var lastException = Server.GetLastError();
            var exception = lastException as HttpException;
            if (exception != null)
            {
                Response.StatusCode = exception.GetHttpCode();
            }
            var errorController = new ErrorController(NinjectMVC3.Kernel.Get<IDIKernel>());
            var model = new ErrorViewModel
                            {
                                ErrorModel = new ErrorModel
                                                 {
                                                     Message =
                                                         lastException.
                                                         Message,
                                                     Code =
                                                         Response.StatusCode
                                                 }
                            };
            var routeData = new RouteData
                                    {
                                        Values =
                                            {
                                                {"controller", "Error"},
                                                {"action", "Index"},
                                                {
                                                    "error_model", model
                                                    }
                                            }
                                    };
            var requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);
            var controllerContext = new ControllerContext(requestContext, errorController);
            var viewResult = errorController.Index(model);
            viewResult.ExecuteResult(controllerContext);
            Server.ClearError();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}