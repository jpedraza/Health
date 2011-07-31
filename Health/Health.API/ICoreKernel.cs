using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;

namespace Health.API
{
    /// <summary>
    /// Базовый сервис, обеспечивающий доступ ко всем репозиториям и другим сервисам
    /// </summary>
    public interface ICoreKernel
    {
        /// <summary>
        /// Репозиторий ролей
        /// </summary>
        IRoleRepository<IRole> RoleRepo { get; }

        /// <summary>
        /// Репозиторий пользователей
        /// </summary>
        IUserRepository<IUser> UserRepo { get; }

        /// <summary>
        /// Репозиторий кандидатов
        /// </summary>
        ICandidateRepository<ICandidate> CandRepo { get; }

        /// <summary>
        /// Сервис авторизации
        /// </summary>
        IAuthorizationService<IUserCredential> AuthServ { get; }

        /// <summary>
        /// Сервис регистрации
        /// </summary>
        IRegistrationService<ICandidate> RegServ { get; }
    }
}