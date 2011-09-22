using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Health.Core;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.API.Validators;
using Health.Core.Entities.POCO;
using Health.Core.Services;
using Health.Data.Repository.Fake;
using Health.Data.Validators;
using Health.Site.Areas.Account.Models.Forms;
using Health.Site.Attributes;
using Health.Site.DI;
using Health.Site.Filters;
using Health.Site.Models;
using Health.Site.Models.Binders;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Providers;
using Health.Site.Repository;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;

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
            Server.ClearError();
            Response.Redirect(@"~/Error");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}