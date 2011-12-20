using System.Collections.Generic;

namespace PrototypeHM.DB
{
    /// <summary>
    /// Маркер классов мапперов.
    /// </summary>
    /// <typeparam name="TResult">Тип результата который возвращает маппер.</typeparam>
    /// <typeparam name="TData">Тип данных получаемых маппером.</typeparam>
    public interface ISqlMapper<TResult, in TData>
        where TResult : class, new()
        where TData : class
    {
        /// <summary>
        /// Сформировать результат.
        /// </summary>
        /// <param name="reader">Набор исходных данных.</param>
        /// <returns>Результат.</returns>
        IList<TResult> Map(TData reader);
    }
}
