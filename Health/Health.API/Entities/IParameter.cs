namespace Health.API.Entities
{
    /// <summary>
    /// Интерфейс некоторого параметра.
    /// </summary>
    public interface IParameter : IEntity
    {
        /// <summary>
        /// Имя параметра.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Мета-данные параметра.
        /// </summary>
        IMetaData[] MetaData { get; set; }
    }
}