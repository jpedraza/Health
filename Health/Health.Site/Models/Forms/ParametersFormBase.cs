using System.Collections.Generic;
using Health.API;
using Health.API.Entities;

namespace Health.Site.Models.Forms
{
    public class ParametersFormBase
    {
        protected ParametersFormBase(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
        }

        public IDIKernel DIKernel { get; protected set; }

        public IEnumerable<IParameter> Parameters { get; set; }
    }
}