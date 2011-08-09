using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;

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
                    _roleRepo = DIKernel.Get<IRoleRepository>(this);
                    Logger.Debug("Репозиторий ролей  инициализирован.");
                }
                return _roleRepo;
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
                    _userRepo = DIKernel.Get<IUserRepository>(this);
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
                    _candRepo = DIKernel.Get<ICandidateRepository>(this);
                    Logger.Debug("Репозиторий кандидатов инициализирован");
                }
                return _candRepo;
            }
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
                    _authServ = DIKernel.Get<IAuthorizationService>(this);
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
                    _regServ = DIKernel.Get<IRegistrationService>(this);
                    Logger.Debug("Сервис регистрации запущен.");
                }

                return _regServ;
            }
        }

        #endregion
    }
}