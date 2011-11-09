using Health.Core.Entities.POCO.Abstract;
using System.Collections.Generic;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Описывает класс диагноза (по международной классификаци)
    /// </summary>
    public class DiagnosisClass : IEntity, IKey
    {
        #region Implementation of Key

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }

        #endregion

        /// <summary>
        /// Имя класса диагноза.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код (номер) класса диагноза
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Блоки диагнозов, входящие в определенный класс диагнозов
        /// </summary>
        public IList<DiagnosisBlock> DiagnosisBlocks { get; set; }
    }
}
