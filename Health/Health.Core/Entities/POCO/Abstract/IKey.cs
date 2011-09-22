using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Core.Entities.POCO.Abstract
{
    /// <summary>
    /// Интерфейс для сущностей с идентификатором.
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        int Id { get; set; }
    }
}
