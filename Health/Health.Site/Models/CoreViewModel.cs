using Health.API;

namespace Health.Site.Models
{
    /// <summary>
    /// Центральный класс моделей представлений
    /// </summary>
    public class CoreViewModel
    {
        public IDIKernel DIKernel { get; set; }

        public ICoreKernel CoreKernel { get; set; }
    }
}