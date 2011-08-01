namespace Health.API.Entities
{
    /// <summary>
    /// Роли по умолчанию.
    /// </summary>
    public interface IDefaultRoles : IEntity
    {
        /// <summary>
        /// Все пользователи.
        /// </summary>
        IRole All { get; set; }

        /// <summary>
        /// Гости.
        /// </summary>
        IRole Guest { get; set; }
    }
}