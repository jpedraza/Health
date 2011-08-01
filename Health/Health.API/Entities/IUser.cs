using System;

namespace Health.API.Entities
{
    /// <summary>
    /// Базовый интерфейс для сущности User.
    /// </summary>
    public interface IUser : IEntity
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Отчество пользвателя.
        /// </summary>
        string ThirdName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        IRole Role { get; set; }

        /// <summary>
        /// День рождения.
        /// </summary>
        DateTime Birthday { get; set; }

        /// <summary>
        /// Токен пользователя (для верификации запомненной сессии).
        /// </summary>
        string Token { get; set; }
    }
}