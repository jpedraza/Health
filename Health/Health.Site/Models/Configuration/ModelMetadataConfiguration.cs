using System;
using System.Collections.Generic;

namespace Health.Site.Models.Configuration
{
    /// <summary>
    /// Класс конфигурации метаданных для каждого свойства модели.
    /// </summary>
    [Serializable]
    public class ModelMetadataConfiguration
    {
        /// <summary>
        /// Свойство модели - его метаданные.
        /// </summary>
        public Dictionary<string, ModelMetadataPropertyConfiguration> Properties { get; set; }

        public ModelMetadataPropertyConfiguration this[string name]
        {
            get { return Properties[name]; }
            set { Properties[name] = value; }
        }
    }
}