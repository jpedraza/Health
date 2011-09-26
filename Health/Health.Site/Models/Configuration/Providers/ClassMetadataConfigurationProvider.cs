using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Models.Providers;

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
        public ClassMetadataConfigurationProvider(IDIKernel di_kernel) : base(di_kernel)
        {
            ConfigurationCache = new Dictionary<Type, ModelMetadataConfiguration>();
            ContainerCache = new Dictionary<string, object>();
        }

        /// <summary>
        /// Конструктор с возможностью указать конкретный тип метаданных для модели.
        /// </summary>
        /// <param name="di_kernel"></param>
        /// <param name="metadata_model_type">Тип метаданных модели.</param>
        public ClassMetadataConfigurationProvider(IDIKernel di_kernel, Type metadata_model_type) : this(di_kernel)
        {
            MetadataModelType = metadata_model_type;
        }

        /// <summary>
        /// Создать тип  метаданных для типа модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="parameters"></param>
        /// <returns>Тип метаданных.</returns>
        private Type CreateMetadataModelType(Type model_type, object[] parameters)
        {
            if (parameters != null && parameters.Length == 1)
            {
                return parameters[0] as Type;
            }
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
        /// <param name="parameters"></param>
        /// <returns>Метаданные для модели или null если их нету.</returns>
        private ModelMetadataConfiguration GetModelMetadata(Type model_type, object[] parameters)
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
            MetadataModelType = CreateMetadataModelType(model_type, parameters);
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
            if (MetadataModelType.BaseType != null && MetadataModelType.BaseType != typeof(object))
            {
                PropertyInfo[] base_properties = MetadataModelType.BaseType.GetProperties();
                foreach (PropertyInfo base_property in base_properties)
                {
                    object[] attributes = base_property.GetCustomAttributes(false);
                    if (!model_metadata.Properties.ContainsKey(base_property.Name))
                    {
                        IList<Attribute> custom_attributes = new BindingList<Attribute>();
                        foreach (object attribute in attributes)
                        {
                            custom_attributes.Add(attribute as Attribute);
                        }
                        var property_metadata = new ModelMetadataPropertyConfiguration {Attributes = custom_attributes};
                        model_metadata.Properties.Add(base_property.Name, property_metadata);
                    }
                    else
                    {
                        foreach (object attribute in attributes)
                        {
                            IList<Attribute> current_attributes = model_metadata.Properties[base_property.Name].Attributes;
                            Attribute current_attribute =
                                current_attributes.Where(a => a.GetType() == attribute.GetType()).FirstOrDefault();
                            if (current_attribute == null)
                            {
                                current_attributes.Add(attribute as Attribute);
                            }
                        }
                    }
                }
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
            string key = String.Format("{0}{1}{2}", container_type == null ? "" : container_type.Name, model_type.Name,
                                       property_name);
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

        #region Implementation of MetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type">Тип контейнера.</param>
        /// <param name="model_accessor">Делегат доступа к модели.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Результат.</returns>
        public override bool IsHaveMetadata(Type container_type, Func<object> model_accessor, Type model_type,
                                            string property_name, params object[] parameters)
        {
            AddToContainerCache(container_type, model_accessor, model_type, property_name);
            if (container_type == null) return false;
            ModelMetadataConfiguration model_configuration = GetModelMetadata(container_type, parameters);
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
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Метаданные для свойства.</returns>
        public override ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor,
                                                                       Type model_type, string property_name,
                                                                       params object[] parameters)
        {
            AddToContainerCache(container_type, model_accessor, model_type, property_name);
            if (container_type == null) return null;
            ModelMetadataConfiguration model_configuration = GetModelMetadata(container_type, parameters);
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