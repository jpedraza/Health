using System.Web.Mvc;
using Health.API;
using Health.API.Entities;

namespace Health.Site.Controllers
{
    public abstract class CoreController : Controller
    {
        protected ICoreKernel _coreKernel;

        protected CoreController(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            ViewBag.DIKernel = DIKernel;
            ViewBag.CoreKernel = CoreKernel;
        }

        public ICoreKernel CoreKernel
        {
            get { return _coreKernel ?? (_coreKernel = DIKernel.Get<ICoreKernel>()); }
        }

        public IDIKernel DIKernel { get; private set; }

        protected TEntity Entity<TEntity>() where TEntity : IEntity
        {
            return DIKernel.Get<TEntity>();
        }
    }
}