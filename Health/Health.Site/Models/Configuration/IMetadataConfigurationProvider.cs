using System;
using System.Collections.Generic;

namespace Health.Site.Models.Configuration
{
    /// <summary>
    /// Провайдер получения метаданных для модели.
    /// </summary>
    public interface IMetadataConfigurationProvider
    {
        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Результат.</returns>
        bool IsHaveMetadata(Type container_type, Func<object> model_accessor, Type model_type, string property_name,
                            params object[] parameters);

        /// <summary>
        /// Получить метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Метаданные для свойства.</returns>
        ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor, Type model_type,
                                                       string property_name, params object[] parameters);

        /// <summary>
        /// Кэш-контейнеров в которых определены свойства модели.
        /// </summary>
        IDictionary<string, object> ContainerCache { get; set; }
    }
}