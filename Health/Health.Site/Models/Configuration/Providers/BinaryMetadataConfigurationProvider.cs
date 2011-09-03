using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using Health.Core.API;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Configuration.Providers
{
    public class BinaryMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
        private readonly IDIKernel _diKernel;

        private Patient PatientModel { get; set; }

        public BinaryMetadataConfigurationProvider() {}

        public BinaryMetadataConfigurationProvider(IDIKernel di_kernel)
        {
            _diKernel = di_kernel;
        }

        private object GetParentObjectContainer(string property_name, Func<object> model_accessor)
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
            if (property_field_info != null) value = container;
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
                        var exp = Expression.Lambda(
                            call, new[] { expression.Parameters[0] });
                        Delegate @delegate = exp.Compile();
                        value = @delegate.DynamicInvoke(container);
                    }
                }
            }
            return value;
        }

        #region Serialization
        /// <summary>
        /// Сериализует метаданные в строку.
        /// </summary>
        /// <param name="model_metadata_configuration">Метаданные.</param>
        /// <returns>Сериализованная строка метаданных.</returns>
        public static string Serialize(ModelMetadataConfiguration model_metadata_configuration)
        {
            var memory_stream = new MemoryStream();
            var binary_serializer = new BinaryFormatter();
            binary_serializer.Serialize(memory_stream, model_metadata_configuration);
            return Encoding.Default.GetString(memory_stream.ToArray());
        }

        /// <summary>
        /// Десериализует метаданные изстроки.
        /// </summary>
        /// <param name="data">Строка для десериализации.</param>
        /// <returns>Метаданные модели.</returns>
        public static ModelMetadataConfiguration Deserialize(string data)
        {
            byte[] byte_data = Encoding.Default.GetBytes(data);
            var memory_stream = new MemoryStream(byte_data);
            var binary_serializer = new BinaryFormatter();
            object obj = binary_serializer.Deserialize(memory_stream);
            if (obj is ModelMetadataConfiguration)
            {
                return obj as ModelMetadataConfiguration;
            }
            throw new Exception("Неверный формат данных для десериализации.");
        }
        #endregion

        #region Implementation of IMetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <returns>Результат.</returns>
        public bool IsHaveMetadata(Type container_type, Func<object> model_accessor, Type model_type, string property_name)
        {
            if (model_type == typeof(Patient) && property_name == "Token") return true;
            return false;
        }

        /// <summary>
        /// Получить метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства.</returns>
        public ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor, Type model_type, string property_name)
        {
            if (model_accessor != null)
            {
                object property_value = model_accessor();
                object parent_container = GetParentObjectContainer(property_name, model_accessor);
                PatientModel = parent_container as Patient;
            }
            return null;
        }

        #endregion
    }
}