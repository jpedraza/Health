using Health.Core.API;

namespace Health.Core
{
    /// <summary>
    /// Центральный класс для всех сервисов и репозиториев.
    /// </summary>
    public class Core : ICore
    {
        protected Core(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
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

        #endregion
    }
}