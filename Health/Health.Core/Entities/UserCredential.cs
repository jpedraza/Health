using System;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities
{
    /// <summary>
    /// Мандат пользователя
    /// </summary>
    [Serializable]
    public class UserCredential : IEntity
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Авторизован ли пользователь
        /// </summary>
        public bool IsAuthorization { get; set; }

        /// <summary>
        /// Запомнен ли пользователь
        /// </summary>
        public bool IsRemember { get; set; }

        /// <summary>
        /// Пользователь совершил быстрый вход?
        /// </summary>
        public bool IsQuickUser { get; set; }
    }
}