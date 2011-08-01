namespace Health.API.Entities
{
    /// <summary>
    /// Базовый интерфейс для сущности Role.
    /// </summary>
    public interface IRole : IEntity
    {
        /// <summary>
        /// Имя роли.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Код роли.
        /// </summary>
        int Code { get; set; }
    }
}