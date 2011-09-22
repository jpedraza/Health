using System;
using System.Collections.Generic;

namespace Health.Core.API
{
    /// <summary>
    /// Интерфейс доступа к DI ядру.
    /// </summary>
    public interface IDIKernel
    {
        /// <summary>
        /// Получить объект по его интерфейсу.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс объекта.</typeparam>
        /// <returns>Объект заданного интерфейса.</returns>
        TObject Get<TObject>();

        /// <summary>
        /// Получить объект по его интерфейсу.
        /// </summary>
        /// <param name="type">Интерфейс объекта.</param>
        /// <returns>Объект заданного интерфейса.</returns>
        object Get(Type type);

        /// <summary>
        /// Получить все объекты связанные с данным сервисом.
        /// </summary>
        /// <param name="service_type"></param>
        /// <returns></returns>
        IEnumerable<object> GetServices(Type service_type);

        /// <summary>
        /// Получить экземпляр объекта с определенным набором параметров конструктора.
        /// </summary>
        /// <param name="type">Тип объекта.</param>
        /// <param name="constructor_parameters">Параметры конструктора.</param>
        /// <returns>Экземпляр объекта.</returns>
        object Get(Type type, params object[] constructor_parameters);
    }
}