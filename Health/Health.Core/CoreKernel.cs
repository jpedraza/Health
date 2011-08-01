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
        private IAuthorizationService<IUserCredential> _authServ;
        private ICandidateRepository<ICandidate> _candRepo;
        private IRegistrationService<ICandidate> _regServ;
        private IRoleRepository<IRole> _roleRepo;
        private IUserRepository<IUser> _userRepo;

        /// <summary>
        /// Создает экземпляр центральное сервиса.
        /// </summary>
        /// <param name="di_kernel">DI ядро.</param>
        public CoreKernel(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
        }

        protected IDIKernel DIKernel { get; set; }

        #region ICoreKernel Members

        /// <summary>
        /// Репозиторий ролей.
        /// </summary>
        public IRoleRepository<IRole> RoleRepo
        {
            get { return _roleRepo ?? (_roleRepo = DIKernel.Get<IRoleRepository<IRole>>(this)); }
        }

        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        public IUserRepository<IUser> UserRepo
        {
            get { return _userRepo ?? (_userRepo = DIKernel.Get<IUserRepository<IUser>>(this)); }
        }

        /// <summary>
        /// Репозиторий кандидатов.
        /// </summary>
        public ICandidateRepository<ICandidate> CandRepo
        {
            get { return _candRepo ?? (_candRepo = DIKernel.Get<ICandidateRepository<ICandidate>>(this)); }
        }

        /// <summary>
        /// Сервис авторизации.
        /// </summary>
        public IAuthorizationService<IUserCredential> AuthServ
        {
            get { return _authServ ?? (_authServ = DIKernel.Get<IAuthorizationService<IUserCredential>>(this)); }
        }

        /// <summary>
        /// Сервис регистрации.
        /// </summary>
        public IRegistrationService<ICandidate> RegServ
        {
            get { return _regServ ?? (_regServ = DIKernel.Get<IRegistrationService<ICandidate>>(this)); }
        }

        #endregion
    }
}