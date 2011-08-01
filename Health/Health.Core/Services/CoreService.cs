using Health.API;
using Health.API.Entities;

namespace Health.Core.Services
{
    /// <summary>
    /// Базовый сервис реализующий общий функцилнал для всех сервисов.
    /// </summary>
    public class CoreService : ICore
    {
        #region ICore Members

        /// <summary>
        /// DI ядро.
        /// </summary>
        public IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Центральное яжро системы.
        /// </summary>
        public ICoreKernel CoreKernel { get; set; }

        /// <summary>
        /// Получть экземпляр сущности пл её интерфейсу.
        /// </summary>
        /// <typeparam name="TInstance">Интерфейс сущности.</typeparam>
        /// <returns>Экземпляр сущности.</returns>
        public TInstance Instance<TInstance>()
            where TInstance : IEntity
        {
            return DIKernel.Get<TInstance>();
        }

        /// <summary>
        /// Инициализация DI ядра и ядра системы.
        /// </summary>
        /// <param name="di_kernel">DI ядро.</param>
        /// <param name="core_kernel">Центральный севис.</param>
        public void SetKernelAndCoreService(IDIKernel di_kernel, ICoreKernel core_kernel)
        {
            DIKernel = di_kernel;
            CoreKernel = core_kernel;
            InitializeData();
        }

        /// <summary>
        /// Инициализация прочих данных.
        /// </summary>
        public virtual void InitializeData()
        {
            return;
        }

        #endregion
    }
}