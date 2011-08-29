﻿using System.Web.Mvc;
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
using Health.Site.Areas.Account.Models.Forms;
using Health.Site.Attributes;
using Health.Site.DI;
using Health.Site.Filters;
using Health.Site.Models;
using Health.Site.Models.Binders;
using Health.Site.Models.Configuration;
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

[assembly: PreApplicationStartMethod(typeof (NinjectMVC3), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectMVC3), "Stop")]

namespace Health.Site.App_Start
{
    public static class NinjectMVC3
    {
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
        /// Регистрация нестандартных биндеров для моделей
        /// </summary>
        public static void ModelToBinder()
        {
            ModelBinders.Binders.Add(typeof (InterviewFormModel), new ParametersFormBinder(Kernel.Get<IDIKernel>()));
        }

        /// <summary>
        /// Регистрация провайдеров метаданных.
        /// </summary>
        public static void ModelProvider()
        {
            var binder = new ModelMetadataProviderBinder();

            binder.Bind<TestModel>().To<ModelMetadataProviderAdapter, ClassMetadataConfigurationProvider>();
            binder.Bind<Patient>().To<ModelMetadataProviderAdapter, SubClassMetadataConfigurationProvider>();

            var manager = new ModelMetadataProviderManager(binder);
            ModelMetadataProviders.Current = manager;
            ModelValidatorProviders.Providers.Add(new ModelValidatorProviderAdapter(binder));
        }

        /// <summary>
        /// Остановка приложения.
        /// </summary>
        public static void Stop()
        {
            _bootstrapper.ShutDown();
        }

        /// <summary>
        /// Создания ядра.
        /// </summary>
        /// <returns>Созданное ядро.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
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
    }
}