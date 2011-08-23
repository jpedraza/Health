using System;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// 
    /// </summary>
    public class Candidate : Entity
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользвателя.
        /// </summary>
        public string ThirdName { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// День рождения.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Токен пользователя (для верификации запомненной сессии).
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Номер полюса.
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// Номер больничной карты.
        /// </summary>
        public string Card { get; set; }
    }
}