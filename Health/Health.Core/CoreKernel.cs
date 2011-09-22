using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;

namespace Health.Core
{
    /// <summary>
    /// Центральный севис.
    /// </summary>
    public class CoreKernel : ICoreKernel
    {
        private IAuthorizationService _authServ;
        private ICandidateRepository _candRepo;
        private IRegistrationService _regServ;
        private IRoleRepository _roleRepo;
        private IUserRepository _userRepo;
        private IParameterRepository _paramRepo;
        private IDefaultScheduleRepository _defaultScheduleRepo;
        private IPersonalScheduleRepository _personalScheduleRepo;
        private IPatientRepository _patientRepo;

        /// <summary>
        /// Создает экземпляр центральное сервиса.
        /// </summary>
        /// <param name="di_kernel">DI ядро.</param>
        public CoreKernel(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            Logger = DIKernel.Get<ILogger>();
            Logger.Debug("Центральное ядро приложения инициализировано.");
        }

        protected IDIKernel DIKernel { get; set; }

        #region ICoreKernel Members

        /// <summary>
        /// Логгер.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Репозиторий ролей.
        /// </summary>
        public IRoleRepository RoleRepo
        {
            get
            {
                if (_roleRepo == null)
                {
                    _roleRepo = DIKernel.Get<IRoleRepository>();
                    Logger.Debug("Репозиторий ролей  инициализирован.");
                }
                return _roleRepo;
            }
        }
        /// <summary>
        /// Репозиторий параметров.
        /// </summary>
        public IParameterRepository ParamRepo
        {
            get
            {
                if (_paramRepo == null)
                {
                    _paramRepo = DIKernel.Get<IParameterRepository>();
                    Logger.Debug("Репозиторий параметров инициализирован.");
                }
                return _paramRepo;
            }
        }
        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        public IUserRepository UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = DIKernel.Get<IUserRepository>();
                    Logger.Debug("Репозиторий пользователей инициализирован.");
                }
                return _userRepo;
            }
        }

        /// <summary>
        /// Репозиторий кандидатов.
        /// </summary>
        public ICandidateRepository CandRepo
        {
            get
            {
                if (_candRepo == null)
                {
                    _candRepo = DIKernel.Get<ICandidateRepository>();
                    Logger.Debug("Репозиторий кандидатов инициализирован");
                }
                return _candRepo;
            }
        }

        /// <summary>
        /// Дефолтный репозиторий расписаний.
        /// </summary>
        public IDefaultScheduleRepository DefaultScheduleRepo
        {
            get
            {
                if (_defaultScheduleRepo == null)
                {
                    _defaultScheduleRepo = DIKernel.Get<IDefaultScheduleRepository>();
                    Logger.Debug("Репозиторий дефолтных расписаний создан.");
                }
                return _defaultScheduleRepo;
            }
            set { _defaultScheduleRepo = value; }
        }

        /// <summary>
        /// Персональный репозиторий расписаний.
        /// </summary>
        public IPersonalScheduleRepository PersonalScheduleRepo
        {
            get
            {
                if (_personalScheduleRepo == null)
                {
                    _personalScheduleRepo = DIKernel.Get<IPersonalScheduleRepository>();
                    Logger.Debug("Репозиторий персональных расписаний создан.");
                }
                return _personalScheduleRepo;
            }
            set { _personalScheduleRepo = value; }
        }

        /// <summary>
        /// Репозиторий пациентов.
        /// </summary>
        public IPatientRepository PatientRepo
        {
            get
            {
                if (_patientRepo == null)
                {
                    _patientRepo = DIKernel.Get<IPatientRepository>();
                    Logger.Debug("Репозиторий пациентов создан.");
                }
                return _patientRepo;
            }
            set { _patientRepo = value; }
        }

        /// <summary>
        /// Сервис авторизации.
        /// </summary>
        public IAuthorizationService AuthServ
        {
            get
            {
                if (_authServ == null)
                {
                    _authServ = DIKernel.Get<IAuthorizationService>();
                    Logger.Debug("Сервис авторизации запущен.");
                }
                return _authServ;
            }
        }

        /// <summary>
        /// Сервис регистрации.
        /// </summary>
        public IRegistrationService RegServ
        {
            get
            {
                if (_regServ == null)
                {
                    _regServ = DIKernel.Get<IRegistrationService>();
                    Logger.Debug("Сервис регистрации запущен.");
                }

                return _regServ;
            }
        }

        #endregion
    }
}