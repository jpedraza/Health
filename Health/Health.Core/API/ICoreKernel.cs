using Health.Core.API.Repository;
using Health.Core.API.Services;

namespace Health.Core.API
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

        #region Repositories

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
        /// Дефолтный репозиторий расписаний.
        /// </summary>
        IDefaultScheduleRepository DefaultScheduleRepo { get; set; }

        /// <summary>
        /// Персональный репозиторий расписаний.
        /// </summary>
        IPersonalScheduleRepository PersonalScheduleRepo { get; set; }

        /// <summary>
        /// Репозиторий пациентов.
        /// </summary>
        IPatientRepository PatientRepo { get; set; }

        #endregion

        #region Services

        /// <summary>
        /// Сервис авторизации.
        /// </summary>
        IAuthorizationService AuthServ { get; }

        /// <summary>
        /// Сервис регистрации.
        /// </summary>
        IRegistrationService RegServ { get; }

        #endregion
    }
}