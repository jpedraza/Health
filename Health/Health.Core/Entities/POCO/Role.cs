using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : IEntity, IKey
    {
        /// <summary>
        /// Имя роли.
        /// </summary>
        public string Name { get; set; }

        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}