﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration
{
    /// <summary>
    /// Класс конфигурации метаданных для каждого свойства модели.
    /// </summary>
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