using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Хирургическое воздействие
    /// </summary>
    public class Surgery : IEntity, IKey
    {
        #region Implementation of Key
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }
        #endregion

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
