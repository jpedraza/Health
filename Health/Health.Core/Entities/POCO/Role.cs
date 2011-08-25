using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : IEntity
    {
        /// <summary>
        /// Имя роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код роли
        /// </summary>
        public int Code { get; set; }
    }
}