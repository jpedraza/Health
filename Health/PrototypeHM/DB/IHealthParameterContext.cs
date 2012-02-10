using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Хранит контекст описаний всех сущностей, связанных с операциями над параметрами здоровья
    /// </summary>
    public interface IHealthParameterContext
    {
        /// <summary>
        /// Проверка объекта на валидность значений
        /// </summary>
        bool IsValid();
    }
}
