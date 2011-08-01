using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.API
{
    /// <summary>
    /// Фабрика валидаторов
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Проверяет значение на валидность согласно типу валидатора
        /// </summary>
        /// <param name="validator_type">Тип валидатора</param>
        /// <param name="value">Значение</param>
        /// <returns>Результата проверки</returns>
        bool IsValid(string validator_type, object value);
    }
}
