using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Специальность доктора.
    /// </summary>
    public class Specialty : IKey, IEntity
    {
        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Имя специальности.
        /// </summary>
        public string Name { get; set; }
    }
}
