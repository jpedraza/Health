using System;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : Entity
    {
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
        public Role Role { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Токен для сессии в куках
        /// </summary>
        public string Token { get; set; }
    }
}