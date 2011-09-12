using System;
using System.Linq.Expressions;
using Health.API;
using Health.API.Entities;

namespace Health.Data
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

        #endregion
    }
}