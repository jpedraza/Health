using Health.Core.API;

namespace Health.Site.Models
{
    /// <summary>
    /// Центральный класс моделей представлений
    /// </summary>
    public class CoreViewModel
    {
        /// <summary>
        /// DI ядро.
        /// </summary>
        public IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Центральное ядро приложения.
        /// </summary>
        public ICoreKernel CoreKernel { get; set; }
    }
}