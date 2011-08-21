using System;
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
using Health.Site.Models;

namespace Health.Site.Controllers
{
    /// <summary>
    /// √лавный класс контроллеров.
    /// </summary>
    public abstract class CoreController : Controller
    {
        protected ICoreKernel _coreKernel;

        /// <summary>
        /// ÷ентральное €дро системы.
        /// </summary>
        public ICoreKernel CoreKernel
        {
            get { return _coreKernel ?? (_coreKernel = DIKernel.Get<ICoreKernel>()); }
        }

        /// <summary>
        /// DI €дро системы.
        /// </summary>
        public IDIKernel DIKernel { get; private set; }

        /// <summary>
        /// Ћоггер.
        /// </summary>
        protected ILogger Logger { get; set; }

        protected CoreController(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            Logger = DIKernel.Get<ILogger>();
        }

        /// <summary>
        /// —оздать экземпл€р сущности по интерфейсу.
        /// </summary>
        /// <typeparam name="TInstance">»нтерфейс сущности.</typeparam>
        /// <returns>Ёкземпл€р сущности.</returns>
        public TInstance Instance<TInstance>()
            where TInstance : IEntity
        {
            Logger.Debug(String.Format("—оздаетс€ сущность дл€ интерфейса - {0}.", typeof(TInstance).Name));
            return DIKernel.Get<TInstance>();
        }

        /// <summary>
        /// —оздать экземпл€р сущности по интерфейсу.
        /// </summary>
        /// <typeparam name="TInstance">»нтерфейс сущности.</typeparam>
        /// <param name="init">»нициализатор сущности.</param>
        /// <returns>Ёкземпл€р сущности.</returns>
        public TInstance Instance<TInstance>(Action<TInstance> init) 
            where TInstance : IEntity
        {
            Logger.Debug(String.Format("—оздаетс€ сущность дл€ интерфейса - {0}.", typeof(TInstance).Name));
            var obj = DIKernel.Get<TInstance>();
            init.Invoke(obj);
            return obj;
        }
    }
}