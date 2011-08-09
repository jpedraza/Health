using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;

namespace Health.API
{
    /// <summary>
    /// Базовый сервис, обеспечивающий доступ ко всем репозиториям и другим сервисам.
    /// </summary>
    public interface ICoreKernel
    {
        /// <summary>
        /// Логгер.
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Репозиторий ролей.
        /// </summary>
        IRoleRepository RoleRepo { get; }

        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        IUserRepository UserRepo { get; }

        /// <summary>
        /// Репозиторий кандидатов.
        /// </summary>
        ICandidateRepository CandRepo { get; }

        /// <summary>
        /// Сервис авторизации.
        /// </summary>
        IAuthorizationService AuthServ { get; }

        /// <summary>
        /// Сервис регистрации.
        /// </summary>
        IRegistrationService RegServ { get; }
    }
}