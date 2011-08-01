namespace Health.API.Entities
{
    /// <summary>
    /// Мета-данные чего-либо.
    /// </summary>
    public interface IMetaData
    {
        /// <summary>
        /// Ключ данных
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Значение данных.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Создать массив мета-данных из чего-либо.
        /// </summary>
        /// <returns>Массив мета-данных.</returns>
        IMetaData[] Create();
    }
}