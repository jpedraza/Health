using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Attributes
{
    /// <summary>
    /// Абстрактный класс для работы с PRG - pattern.
    /// </summary>
    public abstract class PRGModelState : ActionFilterAttribute
    {
        /// <summary>
        /// Ключ состояния модели в хранилище.
        /// </summary>
        public static string ModelStateKey { get; set; }

        /// <summary>
        /// Ключ параметров действия в хранилище. 
        /// </summary>
        public static string PRGParametersKey { get; set; }

        /// <summary>
        /// Префикс для ключа состояния модели.
        /// </summary>
        protected static readonly string ModelStateKeyPrefix = "ModelState_";

        /// <summary>
        /// Префикс для ключа параметров.
        /// </summary>
        protected static readonly string PRGParametersKeyPrefix = "PRGParameters_";

        public static IList<PRGParameter> GetExportModel<T>(Expression<Action<T>> action)
        {
            IList<PRGParameter> prg_parameters = new List<PRGParameter>();
            if (action.Body.NodeType == ExpressionType.Call)
            {
                var action_as_call = action.Body as MethodCallExpression;
                if (action_as_call != null && action_as_call.Arguments.Count > 0)
                {
                    string action_name = action_as_call.Method.Name;
                    ReadOnlyCollection<Expression> arguments = action_as_call.Arguments;
                    foreach (var argument in arguments)
                    {
                        if (argument.NodeType == ExpressionType.MemberAccess)
                        {
                            var member = argument as MemberExpression;
                            if (member != null)
                            {
                                string member_name = member.Member.Name;
                                var property_info = member.Member as FieldInfo;
                                if (property_info != null)
                                {
                                    Type member_type = member.Type;
                                    if (member.Expression.NodeType == ExpressionType.Constant)
                                    {
                                        var member_value_exp = member.Expression as ConstantExpression;
                                        if (member_value_exp != null)
                                        {
                                            object value = property_info.GetValue(member_value_exp.Value);
                                            object member_value = member_value_exp.Value;
                                            Type controller_type = member_value_exp.Type.ReflectedType;
                                            if (controller_type != null)
                                            {
                                                PRGParametersKey = PRGParametersKeyPrefix + action_name +
                                                                   controller_type.Name.Replace("Controller", "");
                                                prg_parameters.Add(new PRGParameter
                                                                       {
                                                                           Name = member_name,
                                                                           Value = value
                                                                       });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return prg_parameters;
        }
    }

    /// <summary>
    /// Атрибут для автоматического экспорта состояния модели в хранилище.
    /// </summary>
    public class PRGExport : PRGModelState
    {
        protected static readonly string TempPRGParametersKey = "PRGParametersTempKey";

        /// <summary>
        /// Перед выполнение действия сохраняем его параметры во временное хранилище.
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filter_context)
        {
            base.OnActionExecuting(filter_context);
            // Если есть параметры...
            if (filter_context.ActionParameters.Count > 0)
            {
                IList<PRGParameter> prg_parameters = new List<PRGParameter>();
                IDictionary<string, object> action_parameters = filter_context.ActionParameters;
                // перебираем их...
                foreach (var action_parameter in action_parameters)
                {
                    var prg_parameter = new PRGParameter
                                            {
                                                Name = action_parameter.Key,
                                                Value = action_parameter.Value
                                            };
                    prg_parameters.Add(prg_parameter);
                }
                // и сохраняем во временное хранилище.
                filter_context.Controller.TempData[TempPRGParametersKey] = prg_parameters;
            }
        }

        /// <summary>
        /// После выполнения действия записываем состояние модели и параметры действия в постоянное хранилище.
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext filter_context)
        {
            base.OnActionExecuted(filter_context);
            // Если модель валидна и если результат действия - редирект на роут...
            if (!filter_context.Controller.ViewData.ModelState.IsValid && filter_context.Result is RedirectToRouteResult)
            {
                var result = filter_context.Result as RedirectToRouteResult;
                // формируем ключи для получения данных их постоянного хранилища.
                ModelStateKey = ModelStateKeyPrefix + result.RouteValues["action"] + result.RouteValues["controller"];
                PRGParametersKey = PRGParametersKeyPrefix + result.RouteValues["action"] + result.RouteValues["controller"];
                // Сохраняем состояние модели в хранилище.
                filter_context.Controller.TempData[ModelStateKey] = filter_context.Controller.ViewData.ModelState;
                filter_context.Controller.TempData[PRGParametersKey] = filter_context.Controller.TempData[TempPRGParametersKey];
            }
        }
    }

    /// <summary>
    /// Атрибут для автоматического импорта модели из хранилища.
    /// </summary>
    public class PRGImport : PRGModelState
    {
        /// <summary>
        /// Перед выполнением действия внедряем сохраненные параметры.
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filter_context)
        {
            base.OnActionExecuting(filter_context);
            // Формируем ключи для доступа к данных...
            PRGParametersKey = PRGParametersKeyPrefix +
                filter_context.ActionDescriptor.ActionName +
                filter_context.ActionDescriptor.ControllerDescriptor.ControllerName;
            ModelStateKey = ModelStateKeyPrefix +
                filter_context.ActionDescriptor.ActionName + 
                filter_context.ActionDescriptor.ControllerDescriptor.ControllerName;
            TempDataDictionary temp_data = filter_context.Controller.TempData;
            // если в хранилище есть данные...
            if (temp_data.ContainsKey(PRGParametersKey) && temp_data[PRGParametersKey] != null)
            {
                // забираем данные как тип...
                var prg_parameters = temp_data[PRGParametersKey] as IList<PRGParameter>;
                // если данные нужного типа...
                if (prg_parameters != null)
                {
                    // берем список параметров метода...
                    ParameterDescriptor[] parameter_descriptors = filter_context.ActionDescriptor.GetParameters();
                    // перебираем их...
                    foreach (var prg_parameter in prg_parameters)
                    {
                        if (parameter_descriptors.Any(p => p.ParameterName == prg_parameter.Name
                                && (p.ParameterType.IsAssignableFrom(prg_parameter.Value.GetType()) || p.ParameterType == prg_parameter.Value.GetType())))
                        {
                            // внедряем параметр в метод.
                            filter_context.ActionParameters[prg_parameter.Name] = prg_parameter.Value;
                        }
                    }
                }
            }
            temp_data.Remove(PRGParametersKey);
        }

        /// <summary>
        /// После выполнения действия производим слияние состояний моделей.
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext filter_context)
        {
            base.OnActionExecuted(filter_context);

            TempDataDictionary temp_data = filter_context.Controller.TempData;
            if (temp_data.ContainsKey(ModelStateKey))
            {
                var model_state = temp_data[ModelStateKey] as ModelStateDictionary;

                if (model_state != null)
                {
                    if (filter_context.Result is ViewResult)
                    {
                        // Производим слияние состояний модели.
                        filter_context.Controller.ViewData.ModelState.Merge(model_state);
                    }
                    else
                    {
                        filter_context.Controller.TempData.Remove(ModelStateKey);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Формат хранения параметров в хранилище.
    /// </summary>
    public class PRGParameter
    {
        /// <summary>
        /// Имя параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра.
        /// </summary>
        public object Value { get; set; }
    }
}