using System.Reflection;
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

    public class MvcApplication : NinjectHttpApplication
    {
        /// <summary>
        /// DI ядро приложения.
        /// </summary>
        protected IDIKernel _diKernel;

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

        /// <summary>
        /// При старте приложения.
        /// </summary>
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            _diKernel = new DIKernel(Kernel);
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            ModelProvider();
            ModelToBinder();
        }

        /// <summary>
        /// Регистрация нестандартных биндеров для моделей
        /// </summary>
        protected void ModelToBinder()
        {
            ModelBinders.Binders.Add(typeof(InterviewFormModel), new ParametersFormBinder(_diKernel));
        }

        /// <summary>
        /// Creates the kernel.
        /// </summary>
        /// <returns>
        /// The kernel.
        /// </returns>
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Регистрация компонентов.
        /// </summary>
        /// <param name="kernel">Ядро.</param>
        protected void RegisterServices(IKernel kernel)
        {
            // Репозитории
            kernel.Bind<IRoleRepository>().To<RolesFakeRepository>().InSingletonScope();
            kernel.Bind<IUserRepository>().To<UsersFakeRepository>().InSingletonScope();
            kernel.Bind<IActualCredentialRepository>().To<SessionRepository>();
            kernel.Bind<IPermanentCredentialRepository>().To<CookieRepository>();
            kernel.Bind<ICandidateRepository>().To<CandidatesFakeRepository>().InSingletonScope();
            kernel.Bind<IDefaultScheduleRepository>().To<DefaultScheduleFakeRepository>().InSingletonScope();
            // Сервисы
            kernel.Bind<ICoreKernel>().To<CoreKernel>().InSingletonScope();
            kernel.Bind<IAuthorizationService>().To<AuthorizationService>();
            kernel.Bind<IRegistrationService>().To<RegistrationService>();
            // Фабрики
            kernel.Bind<IValidatorFactory>().To<ValidatorFactory>();
            // Фильтры для атрибутов
            kernel.BindFilter<AuthFilter>(FilterScope.Controller, 0).WhenActionMethodHas<Auth>().
                WithConstructorArgumentFromActionAttribute<Auth>("allow_roles", att => att.AllowRoles).
                WithConstructorArgumentFromActionAttribute<Auth>("deny_roles", att => att.DenyRoles);
            // Прочее
            kernel.Bind<IDIKernel>().To<DIKernel>();
            kernel.Bind<ILogger>().To<Logger>().WithConstructorArgument("class_name", c => c.Request.Service.Name);
        }

        /// <summary>
        /// Регистрация провайдеров метаданных.
        /// </summary>
        protected void ModelProvider()
        {
            var binder = new ModelMetadataProviderBinder();
            
            binder.AddConfigurationProvider(new XmlMetadataConfigurationProvider(Server.MapPath("~/App_Data/ModelMetadata/")));
            binder.AddConfigurationProvider(new XmlABMetadataConfigurationProvider(Server.MapPath("~/App_Data/ModelMetadata/")));
            binder.AddConfigurationProvider(new BinaryMetadataConfigurationProvider(_diKernel));

            binder.Bind<TestModel>().To<ModelMetadataProviderAdapter, XmlABMetadataConfigurationProvider>();
            binder.Bind<Patient>().To<ModelMetadataProviderAdapter, SubClassMetadataConfigurationProvider>();

            var manager = new ModelMetadataProviderManager(binder);
            ModelMetadataProviders.Current = manager;
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ModelValidatorProviderAdapter(binder));
        }
    }
}