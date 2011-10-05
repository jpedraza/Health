using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    /// <summary>
    /// Главный класс контроллеров.
    /// </summary>
    public abstract class CoreController : Controller
    {
        protected CoreController(IDIKernel diKernel)
        {
            DIKernel = diKernel;
        }

        protected T Get<T>()
        {
            return DIKernel.Get<T>();
        }

        /// <summary>
        /// Копирует свойства одного объекта в одноименные и однотипные свойства другого объекта.
        /// </summary>
        public Mapper Mapper
        {
            get { return DIKernel.Get<Mapper>(); }
        }

        /// <summary>
        /// DI ядро системы.
        /// </summary>
        public IDIKernel DIKernel { get; private set; }

        /// <summary>
        /// Логгер.
        /// </summary>
        protected ILogger Logger
        {
            get { return DIKernel.Get<ILogger>(); }
        }

        /// <summary>
        /// Строготипизированный редирект.
        /// </summary>
        /// <typeparam name="T">Тип контроллера.</typeparam>
        /// <param name="action">Действие контроллера.</param>
        /// <param name="parametersHook">Передача параметров метода при редиректе.</param>
        /// <returns>Результат редиректа.</returns>
        public ActionResult RedirectTo<T>(Expression<Action<T>> action, bool parametersHook = true)
            where T : Controller
        {
            var act = (MethodCallExpression)action.Body;
            var actionNameInAttribute =
                act.Method.GetCustomAttributes(typeof (ActionNameAttribute), true).FirstOrDefault() as
                ActionNameAttribute;
            string actionName = actionNameInAttribute == null ? act.Method.Name : actionNameInAttribute.Name;
            string controllerName = typeof(T).Name.Replace("Controller", "");
            string fullName = typeof(T).FullName;
            string areaName = String.Empty;
            if (fullName != null)
            {
                string[] temp = fullName.Split('.');
                areaName = fullName.Contains("Areas") ? temp[3] : "";
            }
            // получаем результат редиректа...
            RedirectToRouteResult result = RedirectToRoute(new { area = areaName, controller = controllerName, action = actionName });

            // если разрешена передача параметров...
            if (parametersHook)
            {
                var prgModelState = new PRGModelState{ParametersHook = true};
                // получаем список параметров...

                IList<PRGParameter> prgParameters = prgModelState.GetExportModel(action);
                if (prgParameters.Count > 0)
                {
                    // перебираем параметры метода...
                    ParameterInfo[] parameters = act.Method.GetParameters();
                    for (int i = 0; i < prgParameters.Count(); i++)
                    {
                        prgParameters[i].Name = parameters[i].Name;
                        // если параметр помечен PRGInRoute атрибутом...
                        var attr =
                            (PRGInRoute) parameters[i].GetCustomAttributes(typeof (PRGInRoute), false).FirstOrDefault();
                        if (attr != null)
                        {
                            // добавляем параметр в роут...
                            result.RouteValues.Add(prgParameters[i].Name, prgParameters[i].Value);
                        }
                    }
                    // сохраняем текущий список параметров...
                    TempData[prgModelState.PRGParametersKey] = prgParameters;
                }
            }
            // производим редирект...
            return result;
        }
    }
}