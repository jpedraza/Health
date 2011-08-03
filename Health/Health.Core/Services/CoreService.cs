using System;
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
        /// Логгер.
        /// </summary>
        public ILogger Logger { get; set; }

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
            Logger.Debug(String.Format("Создается сущность для интерфейса - {0}.", typeof(TInstance).Name));
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
            Logger = DIKernel.Get<ILogger>();
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