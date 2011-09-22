using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : IEntity
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя роли.
        /// </summary>
        public string Name { get; set; }
    }
}