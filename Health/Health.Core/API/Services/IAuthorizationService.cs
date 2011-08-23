using Health.Core.API.Repository;
using Health.Core.Entities;

namespace Health.Core.API.Services
{
    /// <summary>
    /// Сервис авторизации.
    /// </summary>
    public interface IAuthorizationService : ICoreService
    {
        /// <summary>
        /// Доступ к актуальному хранлищу сессии пользователя.
        /// </summary>
        IActualCredentialRepository ActualDataAccessor { get; set; }

        /// <summary>
        /// Доступ к постоянному хранлищу сессии пользователя.
        /// </summary>
        IPermanentCredentialRepository PermanentDataAccessor { get; set; }

        /// <summary>
        /// Дефолтные роли пользователя.
        /// </summary>
        DefaultRoles DefaultRoles { get; set; }

        /// <summary>
        /// Дефолтное имя переменной в сессии куда сохраняется мандат пользователя.
        /// </summary>
        string DefaultUserCredentialName { get; set; }

        /// <summary>
        /// Дефолтный мандат пользователя.
        /// </summary>
        UserCredential DefaultUserCredential { get; set; }

        /// <summary>
        /// Актуальный мандат пользователя.
        /// </summary>
        UserCredential UserCredential { get; set; }

        /// <summary>
        /// Стартовать сессию.
        /// </summary>
        void SessionStartup();

        /// <summary>
        /// Вход пользователя в систему.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="remember_me">Запоминать?</param>
        /// <returns>Результат авторизации.</returns>
        bool Login(string login, string password, bool remember_me = false);

        /// <summary>
        /// Сброс сессии для пользователя.
        /// </summary>
        void Logout();

        /// <summary>
        /// Запоминаем пользователя.
        /// </summary>
        void RememberMe();

        /// <summary>
        /// Запомнен ли пользователь.
        /// </summary>
        /// <returns>Да или нет.</returns>
        bool IsRemember();

        /// <summary>
        /// Восстановление запомненной сессии.
        /// </summary>
        void RestoreRememberSession();
    }
}