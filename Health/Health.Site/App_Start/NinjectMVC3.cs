using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Hosting;
using System.Web.Mvc;
using Health.Core;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.API.Validators;
using Health.Core.Entities.POCO;
using Health.Core.Services;
using Health.Core.TypeProvider;
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
using Ninject.Modules;
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
            RegisterServices(kernel);
            InitializeData(kernel);
            //_diKernel = new DIKernel(kernel);
            var locator = new NinjectServiceLocator(kernel);
            ServiceLocator.SetLocatorProvider(() => locator);
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
            kernel.Bind<ValidationMetadataRepository>().ToSelf().InRequestScope();
            kernel.Bind<DynamicMetadataRepository>().ToSelf().InRequestScope();
            // ~

            // Сервисы
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

            kernel.BindFilter<ValidationMetadataFilter>(FilterScope.Controller, 0).WhenActionMethodHas
                <ValidationMetadata>().
                WithConstructorArgumentFromActionAttribute<ValidationMetadata>("for", att => att.For).
                WithConstructorArgumentFromActionAttribute<ValidationMetadata>("use", att => att.Use);
            // ~

            // Прочее
            kernel.Bind<IDIKernel>().To<DIKernel>();
            kernel.Bind<ILogger>().To<Logger>().WithConstructorArgument("class_name", c => c.Request.Service.Name);
            // ~

            // Провайдеры метаданных
            /* Биндеры */
            TypeDescriptor.AddProvider(
                new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Period), typeof(PeriodMetadata)), typeof(Period));
            TypeDescriptor.AddProvider(
                new DynamicTypeDescriptorProvider(kernel.Get<IDIKernel>(), typeof(Period)), typeof(Period));

            /* Адаптеры */
            kernel.Bind<ModelMetadataProviderManager>().ToSelf().InRequestScope();

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
            /*ModelMetadataProviders.Current = new ModelMetadataProviderManager(Kernel.Get<IDIKernel>());*/
            /*ModelValidatorProviders.Providers.Clear();            
            ModelValidatorProviders.Providers.Add(new ModelValidatorProviderAdapter(Kernel.Get<IDIKernel>()));*/
        }
        
        internal static void InitializeData(IKernel kernel)
        {
            var doctor1 = new Doctor
            {
                FirstName = "Анатолий",
                LastName = "Петров",
                ThirdName = "Витальевич",
                Login = "doctor",
                Password = "doctor",
                Role = kernel.Get<IRoleRepository>().GetByName("Doctor"),
                Birthday = DateTime.Now,
                Specialty = kernel.Get<ISpecialtyRepository>().GetById(1)
            };

            var patient1 = new Patient
            {
                Birthday = new DateTime(1980, 12, 2),
                Card = "some card number",
                FirstName = "patient1",
                LastName = "patient1",
                Login = "patient1",
                Password = "patient1",
                Policy = "some policy number",
                Role = kernel.Get<IRoleRepository>().GetByName("Patient"),
                ThirdName = "patient1",
                Doctor = doctor1
            };
            doctor1.Patients = new List<Patient> { patient1 };

            var doctor2 = new Doctor
            {
                FirstName = "Анатолий1",
                LastName = "Петров1",
                ThirdName = "Витальевич1",
                Login = "doctor1",
                Password = "doctor1",
                Role = kernel.Get<IRoleRepository>().GetByName("Doctor"),
                Birthday = DateTime.Now,
                Specialty = kernel.Get<ISpecialtyRepository>().GetById(2)
            };

            var patient2 = new Patient
            {
                Birthday = new DateTime(1980, 12, 2),
                Card = "some card number",
                FirstName = "patient2",
                LastName = "patient2",
                Login = "patient2",
                Password = "patient2",
                Policy = "some policy number",
                Role = kernel.Get<IRoleRepository>().GetByName("Patient"),
                ThirdName = "patient2",
                Doctor = doctor2
            };
            doctor2.Patients = new List<Patient> { patient2 };
            kernel.Get<IDoctorRepository>().Save(doctor1);
            kernel.Get<IDoctorRepository>().Save(doctor2);
            var patient_repository = kernel.Get<IPatientRepository>();
            patient_repository.Save(patient1);
            patient_repository.Save(patient2);
        }
    }
}