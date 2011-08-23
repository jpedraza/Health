using System;
using Health.Core.Entities.POCO;

namespace Health.Core.Entities
{
    /// <summary>
    /// Мандат пользователя
    /// </summary>
    [Serializable]
    public class UserCredential : Entity
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
        public bool IsAuthirization { get; set; }

        /// <summary>
        /// Запомнен ли пользователь
        /// </summary>
        public bool IsRemember { get; set; }
    }
}