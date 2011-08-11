using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Core;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    public abstract class CoreController : Controller
    {
        protected ICoreKernel _coreKernel;

        public ICoreKernel CoreKernel
        {
            get { return _coreKernel ?? (_coreKernel = DIKernel.Get<ICoreKernel>()); }
        }

        public IDIKernel DIKernel { get; private set; }

        protected ILogger Logger { get; set; }

        protected CoreController(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            Logger = DIKernel.Get<ILogger>();
            ViewBag.DIKernel = DIKernel;
            ViewBag.CoreKernel = CoreKernel;
        }

        protected override void OnException(ExceptionContext filter_context)
        {
            base.OnException(filter_context);
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

        public TInstance Instance<TInstance>()
            where TInstance : IEntity
        {
            Logger.Debug(String.Format("Создается сущность для интерфейса - {0}.", typeof(TInstance).Name));
            return DIKernel.Get<TInstance>();
        }

        public TInstance Instance<TInstance>(Action<TInstance> init) where TInstance : IEntity
        {
            var obj = DIKernel.Get<TInstance>();
            init.Invoke(obj);
            return obj;
        }
    }
}