using System.Collections.Generic;
using Health.Core.API;
using Health.Core.Entities;
using Health.Core.Entities.POCO;

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
        public IList<Parameter> Parameters { get; set; }
    }
}