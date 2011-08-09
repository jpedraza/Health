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

        protected override void OnResultExecuting(ResultExecutingContext filter_context)
        {
            //InjectDIKernelAndCoreKernelIntoModel(filter_context);
            base.OnResultExecuting(filter_context);
        }

        /// <summary>
        /// Инъекция DI ядра и центрального ядра в модель.
        /// </summary>
        /// <param name="filter_context"></param>
        public void InjectDIKernelAndCoreKernelIntoModel(ResultExecutingContext filter_context)
        {
            Type controller_type = filter_context.Controller.GetType();
            if (controller_type.Namespace != null)
            {
                string name_space =
                    new StringBuilder(controller_type.Namespace).Replace("Controllers", "").Append("Models").ToString();
                if (filter_context.Result.GetType() == typeof (ViewResult))
                {
                    if (filter_context.Result != null)
                    {
                        var result = (ViewResult) filter_context.Result;
                        if (result.Model != null)
                        {
                            var model = (CoreViewModel) result.Model;
                            model.DIKernel = DIKernel;
                            model.CoreKernel = CoreKernel;
                        }
                        else
                        {
                            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                            foreach (Type type in types)
                            {
                                if (type.Namespace != null && type.Namespace.Contains(name_space))
                                {
                                    if (type.IsSubclassOf(typeof (CoreViewModel)) || type == typeof (CoreViewModel))
                                    {
                                        var model = (CoreViewModel) Activator.CreateInstance(type);
                                        model.DIKernel = DIKernel;
                                        model.CoreKernel = CoreKernel;
                                        filter_context.Result = View(result.View, model);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}