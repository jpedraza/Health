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
        /// Редирект.
        /// </summary>
        /// <typeparam name="T">Тип контроллера.</typeparam>
        /// <param name="action">Действие контроллера.</param>
        /// <returns>Результат редиректа. </returns>
        protected ActionResult RedirectTo<T>(Expression<Action<T>> action)
            where T : IController
        {
            var act = (MethodCallExpression) action.Body;
            string name = act.Method.Name;
            return RedirectToAction(name);
        }

        /// <summary>
        /// Редирект.
        /// </summary>
        /// <typeparam name="T">Тип контроллера.</typeparam>
        /// <param name="action">Действие контроллера.</param>
        /// <param name="model">Модель для сохранения в хранилище.</param>
        /// <returns>Результат редиректа. </returns>
        protected ActionResult RedirectTo<T>(Expression<Action<T>> action, object model)
            where T : IController
        {
            TempData["model"] = model;
            return RedirectTo(action);
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