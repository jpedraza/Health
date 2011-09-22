using System;
using Health.API.Entities;

namespace Health.Data.Entities
{
    /// <summary>
    /// Мандат пользователя
    /// </summary>
    [Serializable]
    public class UserCredential : IUserCredential
    {
        #region IUserCredential Members

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

        #endregion
    }
}