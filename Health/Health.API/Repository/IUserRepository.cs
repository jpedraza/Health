using Health.API.Entities;

namespace Health.API.Repository
{
    /// <summary>
    /// Базовый интерфейс репозитория сущности User
    /// </summary>
    public interface IUserRepository<TUser> : ICoreRepository<TUser>
        where TUser : class, IUser
    {
        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Пользователь с заданным логином</returns>
        TUser GetByLogin(string login);

        /// <summary>
        /// Получить пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Пользователь с заданным логином и паролем</returns>
        TUser GetByLoginAndPassword(string login, string password);
    }
}