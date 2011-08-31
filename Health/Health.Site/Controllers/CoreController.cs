using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Reflection;
using System.Web;
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
        private ICoreKernel _coreKernel;

        protected CoreController(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            Logger = DIKernel.Get<ILogger>();
        }

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

        /// <summary>
        /// Строготипизированный редирект.
        /// </summary>
        /// <typeparam name="T">Тип контроллера.</typeparam>
        /// <param name="action">Действие контроллера.</param>
        /// <param name="parameters_hook">Передача параметров метода при редиректе.</param>
        /// <returns>Результат редиректа.</returns>
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
            var act = (MethodCallExpression) action.Body;
            string action_name = act.Method.Name;
            string controller_name = typeof (T).Name.Replace("Controller", "");
            string full_name = typeof (T).FullName;
            string area_name = String.Empty;
            if (full_name != null)
            {
                area_name =
                    full_name.Replace("Health.Site", "").Replace("Areas", "").Replace("Controllers", "").Replace(controller_name, "").
                        Replace(".", "").Replace("Controller", "");
            }
            return RedirectToRoute(new { area = area_name, controller = controller_name, action = action_name });
        }

        /// <summary>
        /// Обрабатывает исключения в контроллерах.
        /// </summary>
        /// <param name="filter_context">Контекст ошибки.</param>
        protected override void OnException(ExceptionContext filter_context)
        {
            // Код ошибки по-умолчанию.
            int code_error = 500;
            string message;
            try
            {
                // Получаем последнюю ошибку
                Exception exception = filter_context.Exception;
                if (exception is HttpException)
                {
                    code_error = (exception as HttpException).GetHttpCode();
                }
                message = exception.Message;
                var error_model = new ErrorViewModel
                                      {
                                          ErrorModel = new ErrorModel
                                                           {
                                                               Code = code_error,
                                                               Message = message
                                                           }
                                      };
                var prg_model_state = new PRGModelState { ParametersHook = true };
                IList<PRGParameter> prg_parameters =
                    prg_model_state.GetExportModel<ErrorController>(a => a.Index(error_model));
                TempData[prg_model_state.PRGParametersKey] = prg_parameters;
            }
            catch (Exception exception)
            {
                code_error = 500;
                message = exception.Message;
            }
            base.OnException(filter_context);
        }
    }
}