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
        /// <param name="parameters_hook">Передача параметров метода при редиректе.</param>
        /// <returns>Результат редиректа.</returns>
        public ActionResult RedirectTo<T>(Expression<Action<T>> action, bool parameters_hook = true)
            where T : Controller
        {
            var act = (MethodCallExpression)action.Body;
            string action_name = act.Method.Name;
            string controller_name = typeof(T).Name.Replace("Controller", "");
            string full_name = typeof(T).FullName;
            string area_name = String.Empty;
            if (full_name != null)
            {
                string[] temp = full_name.Split('.');
                area_name = full_name.Contains("Areas") ? temp[3] : "";
            }
            // получаем результат редиректа...
            RedirectToRouteResult result = RedirectToRoute(new { area = area_name, controller = controller_name, action = action_name });

            // если разрешена передача параметров...
            if (parameters_hook)
            {
                var prg_model_state = new PRGModelState{ParametersHook = true};
                // получаем список параметров...

                IList<PRGParameter> prg_parameters = prg_model_state.GetExportModel(action);
                if (prg_parameters.Count > 0)
                {
                    // перебираем параметры метода...
                    ParameterInfo[] parameters = act.Method.GetParameters();
                    for (int i = 0; i < prg_parameters.Count(); i++)
                    {
                        prg_parameters[i].Name = parameters[i].Name;
                        // если параметр помечен PRGInRoute атрибутом...
                        var attr =
                            (PRGInRoute) parameters[i].GetCustomAttributes(typeof (PRGInRoute), false).FirstOrDefault();
                        if (attr != null)
                        {
                            // добавляем параметр в роут...
                            result.RouteValues.Add(prg_parameters[i].Name, prg_parameters[i].Value);
                        }
                    }
                    // сохраняем текущий список параметров...
                    TempData[prg_model_state.PRGParametersKey] = prg_parameters;
                }
            }
            // производим редирект...
            return result;
        }
    }
}