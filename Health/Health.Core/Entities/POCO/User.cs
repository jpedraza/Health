using System;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : IEntity, IKey
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
        /// Отчество пользователя.
        /// </summary>
        public string ThirdName { get; set; }

        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string FullName
        {
            get { return String.Format("{0} {1} {2}", FirstName, LastName, ThirdName); }
        }

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
        /// Токен для сессии в куках.
        /// </summary>
        public string Token { get; set; }

        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}