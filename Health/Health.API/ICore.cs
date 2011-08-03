using Health.API.Entities;

namespace Health.API
{
    /// <summary>
    /// Интерфейс регламентирует оющий функционал для сервисов и репозиториев.
    /// </summary>
    public interface ICore
    {
        /// <summary>
        /// Логгер.
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// DI ядро.
        /// </summary>
        IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Центральное ядро системы.
        /// </summary>
        ICoreKernel CoreKernel { get; set; }

        /// <summary>
        /// Получить экземпляр объекта, зная его интерфейс.
        /// </summary>
        /// <typeparam name="TInstance">Интерфейс сущности.</typeparam>
        /// <returns>Сущность.</returns>
        TInstance Instance<TInstance>()
            where TInstance : IEntity;

        /// <summary>
        /// Предоставить доступ к DI ядру и центральному ядру системы.
        /// </summary>
        /// <param name="di_kernel">DI ядро.</param>
        /// <param name="core_kernel">Центральное ядро.</param>
        void SetKernelAndCoreService(IDIKernel di_kernel, ICoreKernel core_kernel);

        /// <summary>
        /// Этот метод должем вызываться в методе SetKernelAndCoreService и предоставляет возможность начальной инициализации данных.
        /// </summary>
        void InitializeData();
    }
}