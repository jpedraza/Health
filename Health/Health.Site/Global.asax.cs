using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Health.Core.Entities.POCO;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Health.Site.Models;
using Health.Site.Models.Configuration;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Providers;

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
            Server.ClearError();
            Response.Redirect(@"~/Error");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ModelProvider();
        }

        /// <summary>
        /// Регистрация провайдеров метаданных.
        /// </summary>
        public void ModelProvider()
        {
            var binder = new ModelMetadataProviderBinder();
            
            binder.AddConfigurationProvider(new XmlMetadataConfigurationProvider(Server.MapPath("~/App_Data/ModelMetadata/")));

            binder.Bind<TestModel>().To<ModelMetadataProviderAdapter, XmlMetadataConfigurationProvider>();
            binder.Bind<Patient>().To<ModelMetadataProviderAdapter, SubClassMetadataConfigurationProvider>();

            var manager = new ModelMetadataProviderManager(binder);
            ModelMetadataProviders.Current = manager;
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ModelValidatorProviderAdapter(binder));
        }
    }
}