using System;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    public class Specialty : IKey, IEntity
    {
        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion

        public string Name { get; set; }
    }
}
