using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Site.Models;

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

        protected ActionResult RedirectTo<T>(Expression<Action<T>> action)
            where T : IController
        {
            var act = (MethodCallExpression) action.Body;
            string name = act.Method.Name;
            return RedirectToAction(name);
        }

        protected ActionResult RedirectTo<T>(Expression<Action<T>> action, object model)
            where T : IController
        {
            TempData["model"] = model;
            return RedirectTo(action);
        }
    }
}