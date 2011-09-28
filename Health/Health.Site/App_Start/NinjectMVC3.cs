using System.Web.Hosting;
using System.Web.Mvc;
using Health.Core;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.API.Validators;
using Health.Core.Entities.POCO;
using Health.Core.Services;
using Health.Data.Repository.Fake;
using Health.Data.Validators;
using Health.Site.App_Start;
using Health.Site.Attributes;
using Health.Site.DI;
using Health.Site.Filters;
using Health.Site.Models;
using Health.Site.Models.Binders;
using Health.Site.Models.Configuration;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Providers;
using Health.Site.Repository;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Ninject.Web.Mvc.Validation;
using NinjectAdapter;
using WebActivator;
using Health.Site.Models.Metadata;
using Health.Core.Entities.Virtual;

[assembly: PreApplicationStartMethod(typeof (NinjectMVC3), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectMVC3), "Stop")]

namespace Health.Site.App_Start
{
    public static class NinjectMVC3
    {
        /// <summary>
        /// DI ядро приложения.
        /// </summary>
        private static IDIKernel _diKernel;

        private static readonly Bootstrapper _bootstrapper = new Bootstrapper();

        public static IKernel Kernel { get; private set; }

        /// <summary>
        /// Запуск приложения.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof (HttpApplicationInitializationModule));
            _bootstrapper.Initialize(CreateKernel);
            ModelToBinder();
            ModelProvider();
        }

        /// <summary>
        /// Остановка приложения.
        /// </summary>
        public static void Stop()
        {
            _bootstrapper.ShutDown();
        }

        /// <summary>
        /// Регистрация нестандартных биндеров для моделей
        /// </summary>
        public static void ModelToBinder()
        {
            //ModelBinders.Binders.Add(typeof (InterviewFormModel), new ParametersFormBinder(Kernel.Get<IDIKernel>()));
        }

        /// <summary>
        /// Создания ядра.
        /// </summary>
        /// <returns>Созданное ядро.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            _diKernel = new DIKernel(kernel);
            var locator = new NinjectServiceLocator(kernel);
            ServiceLocator.SetLocatorProvider(() => locator);
            RegisterServices(kernel);
            Kernel = kernel;
            return kernel;
        }

        /// <summary>
        /// Регистрация компонентов.
        /// </summary>
        /// <param name="kernel">Ядро.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Репозитории
            kernel.Bind<IRoleRepository>().To<RolesFakeRepository>().InSingletonScope();
            kernel.Bind<IUserRepository>().To<UsersFakeRepository>().InSingletonScope();
            kernel.Bind<IActualCredentialRepository>().To<SessionRepository>();
            kernel.Bind<IPermanentCredentialRepository>().To<CookieRepository>();
            kernel.Bind<ICandidateRepository>().To<CandidatesFakeRepository>().InSingletonScope();
            kernel.Bind<IParameterRepository>().To<ParametersFakeRepository>().InSingletonScope();
            kernel.Bind<IDefaultScheduleRepository>().To<DefaultScheduleFakeRepository>().InSingletonScope();
            kernel.Bind<IPersonalScheduleRepository>().To<PersonalScheduleFakeRepository>().InSingletonScope();
            kernel.Bind<IPatientRepository>().To<PatientFakeRepository>().InSingletonScope();
            kernel.Bind<IDoctorRepository>().To<DoctorsFakeRepository>().InSingletonScope();
            kernel.Bind<ISpecialtyRepository>().To<SpecialtyFakeRepository>().InSingletonScope();
            // ~

            // Сервисы
            kernel.Bind<ICoreKernel>().To<CoreKernel>().InSingletonScope();
            kernel.Bind<IAuthorizationService>().To<AuthorizationService>().InRequestScope();
            kernel.Bind<IRegistrationService>().To<RegistrationService>().InRequestScope();
            kernel.Bind<IAttendingDoctorService>().To<AttendingDoctorService>().InRequestScope();
            // ~

            // Фабрики
            // ~

            // Фильтры для атрибутов
            kernel.BindFilter<AuthFilter>(FilterScope.Controller, 0).WhenActionMethodHas<Auth>().
                WithConstructorArgumentFromActionAttribute<Auth>("allow_roles", att => att.AllowRoles).
                WithConstructorArgumentFromActionAttribute<Auth>("deny_roles", att => att.DenyRoles);
            // ~

            // Прочее
            kernel.Bind<IDIKernel>().To<DIKernel>();
            kernel.Bind<ILogger>().To<Logger>().WithConstructorArgument("class_name", c => c.Request.Service.Name);
            // ~

            // Провайдеры метаданных
            /* Биндеры */
            kernel.Bind<ModelMetadataProviderBinder>().ToSelf().InRequestScope().
                OnActivation(a => a.For<Patient>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Period>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<DefaultSchedule>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Day>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Month>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Week>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Parameter>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<PersonalSchedule>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<User>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Candidate>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>()).
                OnActivation(a => a.For<Doctor>().Use<MMPAAttributeOnly, ClassMetadataConfigurationProvider>());

            /* Адаптеры */
            //kernel.Bind<ModelMetadataProviderManager>().ToSelf().InRequestScope();

            /* Провайдеры конфигураций */
            kernel.Bind<ClassMetadataConfigurationProvider>().ToSelf().InRequestScope();
            kernel.Bind<SerializerMetadataConfigurationProvider>().ToSelf().InRequestScope();
            kernel.Bind<XmlMetadataConfigurationProvider>().ToSelf().InRequestScope().
                WithConstructorArgument("path", HostingEnvironment.MapPath("~/App_Data/ModelMetadata/"));
            // ~
        }

        /// <summary>
        /// Регистрация провайдеров метаданных.
        /// </summary>
        private static void ModelProvider()
        {
            ModelMetadataProviders.Current = new ModelMetadataProviderManager(_diKernel);
            ModelValidatorProviders.Providers.Clear();            
            ModelValidatorProviders.Providers.Add(new ModelValidatorProviderAdapter(_diKernel));
        }
    }
}