using Health.API.Entities;

namespace Health.Data.Entities
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : Entity, IRole
    {
        #region IRole Members

        /// <summary>
        /// Имя роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код роли
        /// </summary>
        public int Code { get; set; }

        #endregion
    }
}