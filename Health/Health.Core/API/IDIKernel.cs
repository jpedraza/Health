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
        /// Инициализировать объект заданного интерфейса и передать ему DI ядро и центральный сервис.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс объекта.</typeparam>
        /// <param name="core_kernel">Центральный сервис.</param>
        /// <returns>Объект заданного интерфейса.</returns>
        TObject Get<TObject>(ICoreKernel core_kernel)
            where TObject : ICore;

        /// <summary>
        /// Получить все объекты связанные с данным сервисом.
        /// </summary>
        /// <param name="service_type"></param>
        /// <returns></returns>
        IEnumerable<object> GetServices(Type service_type);

        object Get(Type type, params object[] constructor_parameters);
    }
}