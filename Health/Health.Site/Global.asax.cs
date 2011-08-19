using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Health.Site.Controllers;
using Health.Site.Models;

namespace Health.Site
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
        }

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
            try
            {
                // Получаем последнюю ошибку
                Exception exception = Server.GetLastError();
                Response.Clear();

                // Код ошибки по-умолчанию.
                int code_error = 500;

                if (exception is HttpException)
                {
                    code_error = (exception as HttpException).GetHttpCode();
                }
                var controller = new ErrorController();
                ActionResult result = controller.Index(new ErrorViewModel
                                                           {
                                                               ErrorModel = new ErrorModel
                                                                                {
                                                                                    Code = code_error,
                                                                                    Message = "Some message"
                                                                                }
                                                           });
                var route_data = new RouteData();
                route_data.Values.Add("area", "");
                route_data.Values.Add("controller", "Error");
                route_data.Values.Add("action", "Index");
                result.ExecuteResult(
                    new ControllerContext(new RequestContext(new HttpContextWrapper(Context), route_data), controller));
                Server.ClearError();
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.StatusCode = 500;
                HttpContext.Current.Response.Write(exception.Message);
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}