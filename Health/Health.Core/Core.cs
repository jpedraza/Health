using System;
using System.Linq.Expressions;
using Health.API;
using Health.API.Entities;

namespace Health.Core
{
    /// <summary>
    /// Центральный класс для всех сервисов и репозиториев.
    /// </summary>
    public class Core : ICore
    {
        protected Core(IDIKernel di_kernel, ICoreKernel core_kernel)
        {
            DIKernel = di_kernel;
            CoreKernel = core_kernel;
            Logger = DIKernel.Get<ILogger>();
        }

        #region Implementation of ICore

        /// <summary>
        /// Логгер.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// DI ядро.
        /// </summary>
        public IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Центральное ядро системы.
        /// </summary>
        public ICoreKernel CoreKernel { get; set; }

        /// <summary>
        /// Получить экземпляр объекта, зная его интерфейс.
        /// </summary>
        /// <typeparam name="TInstance">Интерфейс сущности.</typeparam>
        /// <returns>Сущность.</returns>
        public TInstance Instance<TInstance>()
            where TInstance : IEntity
        {
            Logger.Debug(String.Format("Создается сущность для интерфейса - {0}.", typeof (TInstance).Name));
            return DIKernel.Get<TInstance>();
        }

        public TInstance Instance<TInstance>(Action<TInstance> init) where TInstance : IEntity
        {
            var obj = DIKernel.Get<TInstance>();
            init.Invoke(obj);
            return obj;
        }

        #endregion
    }
}