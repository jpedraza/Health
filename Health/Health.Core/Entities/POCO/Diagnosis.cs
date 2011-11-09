using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Диагноз.
    /// </summary>
    public class Diagnosis : IEntity, IKey
    {
        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Имя диагноза.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код диагноза по международной классификации
        /// </summary>
        public string Code { get; set; }


    }
}
