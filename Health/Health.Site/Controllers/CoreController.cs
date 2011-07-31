using System;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.API.Services;
using Ninject;

namespace Health.Site.Controllers
{
    public abstract class CoreController : Controller
    {
        protected CoreController(IKernel di_kernel)
        {
            DIKernel = di_kernel;
            ViewData["CoreKernel"] = CoreKernel;
        }

        protected ICoreKernel _coreKernel;
        public ICoreKernel CoreKernel { 
            get { return _coreKernel ?? (_coreKernel = DIKernel.Get<ICoreKernel>()); }
        }

        public IKernel DIKernel { get; private set; }

        protected TEntity Entity<TEntity>() where TEntity : IEntity
        {
            return DIKernel.Get<TEntity>();
        }
    }
}