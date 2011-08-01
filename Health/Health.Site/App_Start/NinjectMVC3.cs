using System.Collections.Generic;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;
using Health.Core;
using Health.Core.Services;
using Health.Data.Entities;
using Health.Data.Repository.Fake;
using Health.Data.Validators;
using Health.Site.App_Start;
using Health.Site.Attributes;
using Health.Site.DI;
using Health.Site.Filters;
using Health.Site.Models.Binders;
using Health.Site.Models.Forms;
using Health.Site.Repository;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using NinjectAdapter;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (NinjectMVC3), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectMVC3), "Stop")]

namespace Health.Site.App_Start
{
    public static class NinjectMVC3
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static IKernel Kernel { get; private set; }

        /// <summary>
        /// Запуск приложения.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof (HttpApplicationInitializationModule));
            Bootstrapper.Initialize(CreateKernel);
            ModelBinders.Binders.Add(typeof (InterviewFormModel), new InterviewFormBinder(Kernel.Get<IDIKernel>()));
        }

        /// <summary>
        /// Остановка приложения.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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
            // Сущности 
            kernel.Bind<IRole>().To<Role>();
            kernel.Bind<IUser>().To<User>();
            kernel.Bind<IDefaultRoles>().To<DefaultRoles>();
            kernel.Bind<IUserCredential>().To<UserCredential>();
            kernel.Bind<IParameter>().To<Parameter>();
            // Репозитории
            kernel.Bind<IRoleRepository<IRole>>().To<RolesFakeRepository<Role>>().InSingletonScope();
            kernel.Bind<IUserRepository<IUser>>().To<UsersFakeRepository<User>>().InSingletonScope();
            kernel.Bind<IActualCredentialRepository>().To<SessionDataAccessor>();
            kernel.Bind<IPermanentCredentialRepository>().To<CookieDataAccessor>();
            kernel.Bind<ICandidateRepository<ICandidate>>().To<CandidatesFakeRepository<Candidate>>().InSingletonScope();
            // Сервисы
            kernel.Bind<ICoreKernel>().To<CoreKernel>().InSingletonScope();
            kernel.Bind<IAuthorizationService<IUserCredential>>().To<AuthorizationService<UserCredential>>();
            kernel.Bind<IRegistrationService<ICandidate>>().To<RegistrationService<Candidate>>();
            // Фабрики
            kernel.Bind<IValidatorFactory>().To<ValidatorFactory>();
            // Фильтры для атрибутов
            kernel.BindFilter<AuthFilter>(FilterScope.Controller, 0).WhenActionMethodHas<Auth>().
                WithConstructorArgumentFromActionAttribute<Auth>("allow_roles", att => att.AllowRoles).
                WithConstructorArgumentFromActionAttribute<Auth>("deny_roles", att => att.DenyRoles);
            // Прочее
            kernel.Bind<IDIKernel>().To<DIKernel>();
            kernel.Bind<IEnumerable<IParameter>>().To<List<Parameter>>();
        }
    }
}