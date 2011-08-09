using System;
using Health.API.Entities;

namespace Health.API
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
        /// Инициализировать объект заданного интерфейса и передать ему DI ядро и центральный сервис.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс объекта.</typeparam>
        /// <param name="core_kernel">Центральный сервис.</param>
        /// <returns>Объект заданного интерфейса.</returns>
        TObject Get<TObject>(ICoreKernel core_kernel)
            where TObject : ICore;

        /// <summary>
        /// Получить экземпляр объекта, зная его интерфейс и инициализатор.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс сущности.</typeparam>
        /// <param name="init">Инициализатор объекта.</param>
        /// <returns>Сущность.</returns>
        TObject Instance<TObject>(Action<TObject> init)
            where TObject : IEntity;
    }
}