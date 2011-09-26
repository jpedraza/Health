using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Базовый интерфейс репозитория сущности User.
    /// </summary>
    public interface IUserRepository : ICoreRepository<User>
    {
        /// <summary>
        /// Получить пользователя по логину.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Пользователь с заданным логином.</returns>
        User GetByLogin(string login);

        /// <summary>
        /// Получить пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Пользователь с заданным логином и паролем.</returns>
        User GetByLoginAndPassword(string login, string password);

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="user_id">Идентификатор пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetById(int user_id);
    }
}