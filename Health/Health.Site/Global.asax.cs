using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Health.Core.API;
using Health.Site.App_Start;
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
                "Default", // Имя маршрута
                "{controller}/{action}/{id}", // URL-адрес с параметрами
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}, // Параметры по умолчанию
                new[] {"Health.Site.Controllers"}
                );

            routes.MapRoute(
                "Home",
                ""
                );

            routes.MapRoute(
                "Admin",
                "Admin/Index"
                );

            routes.MapRoute(
                "Login",
                "Account/Login"
                );
        }

        /// <summary>
        /// Обработка ошибок в приложении.
        /// </summary>
        protected void Application_Error()
        {
            Response.Clear();
            Response.StatusCode = 500;
            var last_exception = Server.GetLastError();
            var exception = last_exception as HttpException;
            if (exception != null)
            {
                Response.StatusCode = exception.GetHttpCode();
            }
            var error_controller = new ErrorController(NinjectMVC3.Kernel.Get<IDIKernel>());
            var model = new ErrorViewModel
                            {
                                ErrorModel = new ErrorModel
                                                 {
                                                     Message =
                                                         last_exception.
                                                         Message,
                                                     Code =
                                                         Response.StatusCode
                                                 }
                            };
            var route_data = new RouteData
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
            var request_context = new RequestContext(new HttpContextWrapper(Context), route_data);
            var controller_context = new ControllerContext(request_context, error_controller);
            var view_result = error_controller.Index(model);
            view_result.ExecuteResult(controller_context);
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