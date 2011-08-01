using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.API;
using Health.API.Entities;
using Ninject;

namespace Health.Site.DI
{
    public class DIKernel : IDIKernel
    {
        protected IKernel Kernel { get; set; }

        public DIKernel(IKernel kernel)
        {
            Kernel = kernel;
        }

        public TObject Get<TObject>()
        {
            return Kernel.Get<TObject>();
        }

        public TObject Get<TObject>(ICoreKernel core_kernel)
            where TObject : ICore
        {
            var obj = Get<TObject>();
            obj.SetKernelAndCoreService(this, core_kernel);
            return obj;
        }
    }
}