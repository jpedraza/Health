using System;
using Health.API.Entities;

namespace Health.Data.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : Entity, IUser
    {
        #region IUser Members

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string ThirdName { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public IRole Role { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Токен для сессии в куках
        /// </summary>
        public string Token { get; set; }

        #endregion
    }
}