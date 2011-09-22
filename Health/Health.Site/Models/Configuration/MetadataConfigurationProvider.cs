using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Health.Core.API;

namespace Health.Site.Models.Configuration
{
    /// <summary>
    /// Базовый класс провайдеров конфигураций.
    /// </summary>
    public abstract class MetadataConfigurationProvider// : MetadataConfigurationProvider
    {
        protected readonly IDIKernel _diKernel;

        protected MetadataConfigurationProvider(IDIKernel di_kernel)
        {
            _diKernel = di_kernel;
        }

        /// <summary>
        /// Получить контейнер в которов определено свойсво модели.
        /// </summary>
        /// <param name="property_name">Свойство модели.</param>
        /// <param name="model_accessor">Делегат доступа к модели.</param>
        /// <returns>Родительский контейнер.</returns>
        protected object GetParentObjectContainer(string property_name, Func<object> model_accessor)
        {
            object value = null;
            if (property_name == null ||
                model_accessor == null ||
                model_accessor.Target == null) return null;

            FieldInfo container_filed_info = model_accessor.Target.GetType().GetField("container");
            if (container_filed_info == null) return null;
            object container = model_accessor.Target.GetType().GetField("container").GetValue(model_accessor.Target);
            if (container == null) return null;

            FieldInfo property_field_info = model_accessor.Target.GetType().GetField("property");
            if (property_field_info != null)
            {
                value = container;
            }
            FieldInfo expression_filed_info = model_accessor.Target.GetType().GetField("expression");
            if (expression_filed_info != null)
            {
                var expression = expression_filed_info.GetValue(model_accessor.Target) as LambdaExpression;
                if (expression == null) return null;
                var member_access_expression = expression.Body as MemberExpression;
                if (member_access_expression != null)
                {
                    var call = member_access_expression.Expression as MethodCallExpression;
                    if (call != null)
                    {
                        LambdaExpression exp = Expression.Lambda(
                            call, new[] {expression.Parameters[0]});
                        Delegate @delegate = exp.Compile();
                        value = @delegate.DynamicInvoke(container);
                    }
                    var parameter = member_access_expression.Expression as ParameterExpression;
                    if (parameter != null)
                    {
                        LambdaExpression exp = Expression.Lambda(parameter, new[] {parameter});
                        Delegate @delegate = exp.Compile();
                        value = @delegate.DynamicInvoke(container);
                    }
                }
            }
            return value;
        }

        #region Implementation of MetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Результат.</returns>
        public abstract bool IsHaveMetadata(Type container_type, Func<object> model_accessor, Type model_type,
                                            string property_name, params object[] parameters);

        /// <summary>
        /// Получить метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Метаданные для свойства.</returns>
        public abstract ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor,
                                                                       Type model_type, string property_name,
                                                                       params object[] parameters);

        /// <summary>
        /// Кэш-контейнеров в которых определены свойства модели.
        /// </summary>
        public IDictionary<string, object> ContainerCache { get; set; }

        #endregion
    }
}