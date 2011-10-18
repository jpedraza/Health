using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Health.Core;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.Entities.POCO;
using Health.Core.Services;
using Health.Core.TypeProvider;
using Health.Data.Repository.Fake;
using Health.Site.App_Start;
using Health.Site.Attributes;
using Health.Site.DI;
using Health.Site.Filters;
using Health.Site.Repository;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
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
            RegisterServices(kernel);
            InitializeData(kernel);
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
            kernel.Bind<DynamicMetadataRepository>().ToSelf().InRequestScope();
            kernel.Bind<IWorkWeekRepository>().To<WorkWeekFakeRepository>().InSingletonScope();
            kernel.Bind<IAppointmentRepository>().To<AppointmentFakeRepository>().InSingletonScope();
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
                WithConstructorArgumentFromActionAttribute<Auth>("allowRoles", att => att.AllowRoles).
                WithConstructorArgumentFromActionAttribute<Auth>("denyRoles", att => att.DenyRoles);

            kernel.BindFilter<ValidationModelAttributeFilter>(FilterScope.Controller, 0).WhenActionMethodHas
                <ValidationModelAttribute>().
                WithConstructorArgumentFromActionAttribute<ValidationModelAttribute>("for", att => att.For).
                WithConstructorArgumentFromActionAttribute<ValidationModelAttribute>("use", att => att.Use).
                WithConstructorArgumentFromActionAttribute<ValidationModelAttribute>("alias", att => att.Alias);
            // ~

            // Прочее
            kernel.Bind<IDIKernel>().To<DIKernel>();
            kernel.Bind<ILogger>().To<Logger>().WithConstructorArgument("class_name", c => c.Request.Service.Name);
            // ~

            // Провайдеры метаданных
            AddDisplayMetadata<Period, PeriodMetadata>();
            AddDynamicMetadata<Period>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<Week, WeekMetadata>();
            AddDynamicMetadata<Week>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<Candidate, CandidateMetadata>();
            AddDynamicMetadata<Candidate>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<Patient, PatientMetadata>();
            AddDynamicMetadata<Patient>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<Doctor, DoctorMetadata>();
            AddDynamicMetadata<Doctor>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<PersonalSchedule, PersonalScheduleMetadata>();
            AddDynamicMetadata<PersonalSchedule>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<DefaultSchedule, DefaultScheduleMetadata>();
            AddDynamicMetadata<DefaultSchedule>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<Parameter, ParameterMetadata>();
            AddDynamicMetadata<Parameter>(kernel.Get<IDIKernel>());

            AddDisplayMetadata<Day, DayMetadata>();
            AddDisplayMetadata<Month, MonthMetadata>();

            AddDisplayMetadata<WorkDay, WorkDayMetadata>();
            AddDynamicMetadata<WorkDay>(kernel.Get<IDIKernel>());
            // ~
        }

        internal static void AddDisplayMetadata<TModel, TMetadata>()
        {
            TypeDescriptor.AddProvider(
                new AssociatedMetadataTypeTypeDescriptionProvider(typeof(TModel), typeof(TMetadata)), typeof(TModel));
        }

        internal static void AddDynamicMetadata<TModel>(IDIKernel diKernel)
        {
            TypeDescriptor.AddProvider(
                new DynamicTypeDescriptorProvider(diKernel, typeof(TModel)), typeof(TModel));
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
            var patientRepository = kernel.Get<IPatientRepository>();
            patientRepository.Save(patient1);
            patientRepository.Save(patient2);

            kernel.Get<IWorkWeekRepository>().Save(new WorkWeek
                                                       {
                                                           Doctor = kernel.Get<IDoctorRepository>().GetById(1),
                                                           WorkDays = new List<WorkDay>
                                                                          {
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Monday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(14, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  },
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Tuesday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(9, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(17, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  },
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Wednesday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(14, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  },
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Thursday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(14, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  },
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Friday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(14, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  },
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Sunday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(14, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  },
                                                                              new WorkDay
                                                                                  {
                                                                                      Day = DaysInWeek.Saturday,
                                                                                      TimeStart = new TimeSpan(6, 0, 0),
                                                                                      TimeEnd = new TimeSpan(15, 0, 0),
                                                                                      DinnerStart =
                                                                                          new TimeSpan(10, 0, 0),
                                                                                      DinnerEnd = new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursStart =
                                                                                          new TimeSpan(11, 0, 0),
                                                                                      AttendingHoursEnd =
                                                                                          new TimeSpan(14, 0, 0),
                                                                                      AttendingMinutes = 15
                                                                                  }
                                                                          }
                                                       });
            doctor1.WorkWeek = kernel.Get<IWorkWeekRepository>().GetByDoctorId(1);
            kernel.Get<IDoctorRepository>().Update(doctor1);
        }
    }
}