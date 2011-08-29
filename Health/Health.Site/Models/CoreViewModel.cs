using System.ComponentModel.DataAnnotations;
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
        [NotMapped]
        protected IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Центральное ядро приложения.
        /// </summary>
        [NotMapped]
        protected ICoreKernel CoreKernel { get; set; }
    }
}