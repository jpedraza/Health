using System.Collections.Generic;
using Health.API;
using Health.API.Entities;

namespace Health.Site.Models.Forms
{
    /// <summary>
    /// Базовый класс для параметрических форм.
    /// </summary>
    public class ParametersFormBase
    {
        protected ParametersFormBase(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
        }

        /// <summary>
        /// DI ядро.
        /// </summary>
        protected IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Список параметров.
        /// </summary>
        public IList<IParameter> Parameters { get; set; }
    }
}