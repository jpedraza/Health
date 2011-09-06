using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Configuration.Providers
{
    /// <summary>
    /// Поставщик метаданных модели напрямую из класса.
    /// </summary>
    public class ClassMetadataConfigurationProvider : MetadataConfigurationProvider
    {
        /// <summary>
        /// Текущий тип метаданных модели.
        /// </summary>
        protected Type MetadataModelType { get; set; }

        /// <summary>
        /// Кэш-конфигураций.
        /// </summary>
        private IDictionary<Type, ModelMetadataConfiguration> ConfigurationCache { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public ClassMetadataConfigurationProvider()
        {
            ConfigurationCache = new Dictionary<Type, ModelMetadataConfiguration>();
            ContainerCache = new Dictionary<string, object>();
        }

        /// <summary>
        /// Конструктор с возможностью указать конкретный тип метаданных для модели.
        /// </summary>
        /// <param name="metadata_model_type">Тип метаданных модели.</param>
        public ClassMetadataConfigurationProvider(Type metadata_model_type) : this() 
        {
            MetadataModelType = metadata_model_type;
        }

        /// <summary>
        /// Создать тип  метаданных для типа модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Тип метаданных.</returns>
        private Type CreateMetadataModelType(Type model_type)
        {
            if (MetadataModelType != null) return MetadataModelType;
            string metadata_model_type_name = String.Format("{0}Metadata", model_type.Name);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] assembly_types = assembly.GetTypes();
            foreach (Type assembly_type in assembly_types)
            {
                if (assembly_type.Name == metadata_model_type_name)
                {
                    return assembly_type;
                }
            }
            return null;
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
            var model_metadata = new ModelMetadataConfiguration
                                     {
                                         Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>()
                                     };
            MetadataModelType = CreateMetadataModelType(model_type);
            if (MetadataModelType == null)
            {
                return model_metadata;
            }
            PropertyInfo[] properties = MetadataModelType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var property_metadata = new ModelMetadataPropertyConfiguration();
                IList<Attribute> custom_attributes = new BindingList<Attribute>();
                object[] attributes = property.GetCustomAttributes(false);
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

        /// <summary>
        /// Кэш-контейнеров для свойств модели.
        /// </summary>
        /// <param name="container_type">Тип контейнера.</param>
        /// <param name="model_accessor">Делегат доступа к модели.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        protected void AddToContainerCache(Type container_type, Func<object> model_accessor, Type model_type,
                                           string property_name)
        {
            string key = String.Format("{0}{1}{2}", container_type == null ? "" : container_type.Name, model_type.Name, property_name);
            object value = GetParentObjectContainer(property_name, model_accessor);
            if (value == null) return;
            if (ContainerCache.ContainsKey(key))
            {
                ContainerCache[key] = value;
            }
            else
            {
                ContainerCache.Add(key, value);
            }
        }

        #region Implementation of IMetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type">Тип контейнера.</param>
        /// <param name="model_accessor">Делегат доступа к модели.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <returns>Результат.</returns>
        public override bool IsHaveMetadata(Type container_type, Func<object> model_accessor, Type model_type,
                                   string property_name)
        {
            AddToContainerCache(container_type, model_accessor, model_type, property_name);
            ModelMetadataConfiguration model_configuration = GetModelMetadata(model_type);
            if (model_configuration != null && model_configuration.Properties != null &&
                model_configuration.Properties.Count > 0)
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
        /// <param name="container_type">Тип контейнера.</param>
        /// <param name="model_accessor">Делегат доступа к модели.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства.</returns>
        public override ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor,
                                                              Type model_type, string property_name)
        {            
            AddToContainerCache(container_type, model_accessor, model_type, property_name);
            ModelMetadataConfiguration model_configuration = GetModelMetadata(model_type);
            if (model_configuration != null && model_configuration.Properties != null &&
                model_configuration.Properties.Count > 0)
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