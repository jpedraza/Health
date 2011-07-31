using Health.API.Entities;
using Health.API.Repository;
using Ninject;

namespace Health.API.Services
{
    public interface IAuthorizationService<TUserCredential> : ICore
        where TUserCredential : IUserCredential
    {
        /// <summary>
        /// Доступ к актуальному хранлищу сессии пользователя
        /// </summary>
        IActualCredentialRepository ActualDataAccessor { get; set; }

        /// <summary>
        /// Доступ к постоянному хранлищу сессии пользователя
        /// </summary>
        IPermanentCredentialRepository PermanentDataAccessor { get; set; }

        /// <summary>
        /// Дефолтные роли пользователя
        /// </summary>
        IDefaultRoles DefaultRoles { get; set; }

        /// <summary>
        /// Дефолтное имя переменной в сессии куда сохраняется мандат пользователя
        /// </summary>
        string DefaultUserCredentialName { get; set; }

        /// <summary>
        /// Дефолтный мандат пользователя
        /// </summary>
        IUserCredential DefaultUserCredential { get; set; }

        /// <summary>
        /// Актуальный мандат пользователя
        /// </summary>
        IUserCredential UserCredential { get; set; }

        /// <summary>
        /// Стартовать сессию
        /// </summary>
        void SessionStartup();

        bool Login(string login, string password, bool remember_me = false);

        /// <summary>
        /// Сброс сессии для пользователя
        /// </summary>
        void Logout();

        /// <summary>
        /// Запоминаем пользователя
        /// </summary>
        void RememberMe();

        /// <summary>
        /// Запомнен ли пользователь
        /// </summary>
        /// <returns>Да или нет :)</returns>
        bool IsRemember();

        /// <summary>
        /// Восстановление запомненной сессии
        /// </summary>
        void RestoreRememberSession();
    }
}