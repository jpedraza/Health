﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <returns>Результат.</returns>
        bool IsHaveMetadata(Type model_type, string property_name);

        /// <summary>
        /// Получить метаданные для свойства модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства.</returns>
        ModelMetadataPropertyConfiguration GetMetadata(Type model_type, string property_name);
    }
}
