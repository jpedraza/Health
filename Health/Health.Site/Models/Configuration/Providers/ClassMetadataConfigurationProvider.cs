using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration.Providers
{
    /// <summary>
    /// Поставщик метаданных модели напрямую из класса.
    /// </summary>
    public class ClassMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
        /// <summary>
        /// Кэш-конфигураций.
        /// </summary>
        private IDictionary<Type, ModelMetadataConfiguration> ConfigurationCache { get; set; }

        public ClassMetadataConfigurationProvider()
        {
            ConfigurationCache = new Dictionary<Type, ModelMetadataConfiguration>();
        }

        /// <summary>
        /// Получить метаданные для модели и попутно занести их в кэш.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Метаданные для модели или null если их нету.</returns>
        private ModelMetadataConfiguration GetModelMetadata(Type model_type)
        {
            foreach (var configuration in ConfigurationCache)
            {
                if (configuration.Key == model_type)
                {
                    return configuration.Value;
                }
            }
            string metadata_model_type_name = String.Format("Health.Site.Models.Metadata.{0}Metadata, Health.Site",
                                                       model_type.Name);
            Type metadata_model_type = Type.GetType(metadata_model_type_name);
            var model_metadata = new ModelMetadataConfiguration
                                     {
                                         Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>()
                                     };
            if (metadata_model_type == null)
            {
                return model_metadata;
            }
            PropertyInfo[] properties = metadata_model_type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var property_metadata = new ModelMetadataPropertyConfiguration();
                IList<Attribute> custom_attributes = new BindingList<Attribute>();
                object[] attributes = property.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    custom_attributes.Add(attribute as Attribute);
                }
                property_metadata.Attributes = custom_attributes;
                model_metadata.Properties.Add(property.Name, property_metadata);
            }
            ConfigurationCache.Add(model_type, model_metadata);
            return model_metadata;
        }

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
            ModelMetadataConfiguration model_configuration = GetModelMetadata(model_type);
            if (model_configuration != null && model_configuration.Properties != null && model_configuration.Properties.Count > 0)
            {
                IDictionary<string, ModelMetadataPropertyConfiguration> properties = model_configuration.Properties;
                foreach (var property in properties)
                {
                    if (property.Key == property_name)
                    {
                        return true;
                    }
                }
            }
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
            ModelMetadataConfiguration model_configuration = GetModelMetadata(model_type);
            if (model_configuration != null && model_configuration.Properties != null && model_configuration.Properties.Count > 0)
            {
                IDictionary<string, ModelMetadataPropertyConfiguration> properties = model_configuration.Properties;
                foreach (var property in properties)
                {
                    if (property.Key == property_name)
                    {
                        return property.Value;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}