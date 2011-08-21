using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Health.API;
using Health.API.Entities;
using Health.Core;
using Health.Site.Areas.Account.Controllers;
using Health.Site.Areas.Account.Models;
using Health.Site.Attributes;
using Health.Site.Models;
using Microsoft.Web.Mvc;

namespace Health.Site.Controllers
{
    /// <summary>
    /// Главный класс контроллеров.
    /// </summary>
    public abstract class CoreController : Controller
    {
        protected ICoreKernel _coreKernel;

        /// <summary>
        /// Центральное ядро системы.
        /// </summary>
        public ICoreKernel CoreKernel
        {
            get { return _coreKernel ?? (_coreKernel = DIKernel.Get<ICoreKernel>()); }
        }

        /// <summary>
        /// DI ядро системы.
        /// </summary>
        public IDIKernel DIKernel { get; private set; }

        /// <summary>
        /// Логгер.
        /// </summary>
        protected ILogger Logger { get; set; }

        protected CoreController(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            Logger = DIKernel.Get<ILogger>();
        }

        /// <summary>
        /// Создать экземпляр сущности по интерфейсу.
        /// </summary>
        /// <typeparam name="TInstance">Интерфейс сущности.</typeparam>
        /// <returns>Экземпляр сущности.</returns>
        public TInstance Instance<TInstance>()
            where TInstance : IEntity
        {
            Logger.Debug(String.Format("Создается сущность для интерфейса - {0}.", typeof(TInstance).Name));
            return DIKernel.Get<TInstance>();
        }

        public ActionResult RedirectTo<T>(Expression<Action<T>> action, bool parameters_hook = false)
            where T : Controller
        {
            if (parameters_hook)
            {
                var prg_model_state = new PRGModelState
                                          {
                                              ParametersHook = true
                                          };
                IList<PRGParameter> prg_parameters = prg_model_state.GetExportModel(action);
                TempData[prg_model_state.PRGParametersKey] = prg_parameters;
            }
            var act = (MethodCallExpression)action.Body;
            return RedirectToAction(act.Method.Name);
        }

        /// <summary>
        /// Создать экземпляр сущности по интерфейсу.
        /// </summary>
        /// <typeparam name="TInstance">Интерфейс сущности.</typeparam>
        /// <param name="init">Инициализатор сущности.</param>
        /// <returns>Экземпляр сущности.</returns>
        public TInstance Instance<TInstance>(Action<TInstance> init) 
            where TInstance : IEntity
        {
            Logger.Debug(String.Format("Создается сущность для интерфейса - {0}.", typeof(TInstance).Name));
            var obj = DIKernel.Get<TInstance>();
            init.Invoke(obj);
            return obj;
        }
    }
}