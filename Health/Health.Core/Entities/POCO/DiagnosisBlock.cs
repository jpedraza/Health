using Health.Core.Entities.POCO.Abstract;
using System.Collections.Generic;

namespace Health.Core.Entities.POCO
{
    public class DiagnosisBlock : IEntity, IKey
    {
        #region Implementation of Key

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Имя блока диагнозов
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код блока диагнозов
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Диагнозы, входящие в определенный блок диагнозов
        /// </summary>
        public IList<Diagnosis> Diagnosises { get; set; }
    }
}
