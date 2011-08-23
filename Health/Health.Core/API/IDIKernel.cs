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
        /// Инициализировать объект заданного интерфейса и передать ему DI ядро и центральный сервис.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс объекта.</typeparam>
        /// <param name="core_kernel">Центральный сервис.</param>
        /// <returns>Объект заданного интерфейса.</returns>
        TObject Get<TObject>(ICoreKernel core_kernel)
            where TObject : ICore;
    }
}