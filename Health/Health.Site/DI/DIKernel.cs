using Health.API;
using Ninject;

namespace Health.Site.DI
{
    public class DIKernel : IDIKernel
    {
        public DIKernel(IKernel kernel)
        {
            Kernel = kernel;
        }

        protected IKernel Kernel { get; set; }

        #region IDIKernel Members

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

        #endregion
    }
}