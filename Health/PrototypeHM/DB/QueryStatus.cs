using PrototypeHM.DB.Attributes;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Статус выполнения запроса к источнику данных.
    /// </summary>
    public class QueryStatus : IQueryResult
    {
        /// <summary>
        /// Статус.
        /// </summary>
        [NotDisplay, NotEdit]
        public int Status { get; set; }

        /// <summary>
        /// Сообщение статуса.
        /// </summary>
        [NotDisplay, NotEdit]
        public string StatusMessage { get; set; }
    }
}
