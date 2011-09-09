using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Models.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Адаптер для привязки метаданных модели из провайдера конфигурации.
    /// </summary>
    public abstract class ModelMetadataProviderAdapter : DataAnnotationsModelMetadataProvider
    {
        /// <summary>
        /// Биндер.
        /// </summary>
        protected readonly ModelMetadataProviderBinder Binder;

        /// <summary>
        /// Провайдер конйигурации.
        /// </summary>
        protected readonly IMetadataConfigurationProvider ConfigurationProvider;

        /// <summary>
        /// DI ядро.
        /// </summary>
        protected readonly IDIKernel DIKernel;

        protected ModelMetadataProviderAdapter(IDIKernel di_kernel,
                                               IMetadataConfigurationProvider configuration_provider)
        {
            DIKernel = di_kernel;
            ConfigurationProvider = configuration_provider;
            Binder = di_kernel.Get<ModelMetadataProviderBinder>();
        }

        #region Overrides of AssociatedMetadataProvider

        /// <summary>
        /// Создать метаданные для свойства модели.
        /// </summary>
        /// <param name="attributes">Атрибуты свойства.</param>
        /// <param name="container_type">Тип контейнера.</param>
        /// <param name="model_accessor">Функция доступа к модели.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства модели.</returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type container_type,
                                                        Func<object> model_accessor, Type model_type,
                                                        string property_name)
        {
            // Создать базовый набор метаданных...
            var model_metadata = new ModelMetadata(this, container_type, model_accessor, model_type,
                                                   property_name);

            
            //если в поставщике метаданных есть метаданные для типа модели с заданным свойством...
            if (ConfigurationProvider.IsHaveMetadata(container_type, model_accessor, model_type, property_name, 
                Binder.GetConfigurationParametersByModelType(container_type)))
            {
                //получить метаданные для свойства...
                ModelMetadataPropertyConfiguration meta = ConfigurationProvider.GetMetadata(container_type,
                                                                                            model_accessor,
                                                                                            container_type,
                                                                                            property_name,
                                                                                            Binder.GetConfigurationParametersByModelType(container_type));

                if (meta != null)
                {
                    model_metadata = InitializeMetadata(model_metadata, meta, attributes, container_type,
                                                        model_accessor, model_type, property_name);
                }
            }
            return model_metadata;
        }

        protected abstract ModelMetadata InitializeMetadata(ModelMetadata model_metadata,
                                                            ModelMetadataPropertyConfiguration property_configuration,
                                                            IEnumerable<Attribute> attributes, Type container_type,
                                                            Func<object> model_accessor, Type model_type,
                                                            string property_name);

        protected ModelMetadata InitializeAttributes(ModelMetadata model_metadata,
                                                     ModelMetadataPropertyConfiguration property_configuration,
                                                     Type container_type,
                                                     Func<object> model_accessor, Type model_type,
                                                     string property_name)
        {
            if (model_metadata == null) throw new ArgumentNullException("model_metadata");
            IList<Attribute> attributes = property_configuration.Attributes;
            model_metadata = base.CreateMetadata(attributes, container_type,
                                                 model_accessor,
                                                 model_type, property_name);
            /*foreach (Attribute attribute in attributes)
            {
                if (attribute.GetType() == typeof(RequiredAttribute))
                {*/
                    model_metadata.IsRequired = false;
                /*}
            }*/
            return model_metadata;
        }

        protected ModelMetadata InitializeProperties(ModelMetadata model_metadata,
                                                     ModelMetadataPropertyConfiguration property_configuration)
        {
            PropertyInfo[] property_infos = typeof (ModelMetadataPropertyConfiguration).GetProperties();

            // обходим свойства...
            foreach (PropertyInfo property_info in property_infos)
            {
                // получаем соответствующее свойство из класса метаданных модели...
                PropertyInfo metadata_property = typeof (ModelMetadata).GetProperty(property_info.Name);

                // если свойство определено в стандартной модели метаданных...
                if (metadata_property != null)
                {
                    // если это не словарь дополнительных параметров...
                    if (property_info.PropertyType != typeof (Dictionary<string, object>))
                    {
                        // получаем значение свойства из провайдера конфигурации...
                        object value = Convert.ChangeType(
                            typeof (ModelMetadataPropertyConfiguration).GetProperty(property_info.Name).
                                GetValue
                                (property_configuration, null),
                            metadata_property.PropertyType);
                        // заносим значение в метаданные модели.
                        metadata_property.SetValue(model_metadata, value, null);
                    }
                        // в ином случае...
                    else
                    {
                        // получаем словарь дополнительных метаданных из провайдера конфигурации...
                        var additional_val =
                            typeof (ModelMetadataPropertyConfiguration).GetProperty(property_info.Name).
                                GetValue
                                (property_configuration, null) as
                            Dictionary<string, object>;

                        if (additional_val != null)
                        {
                            // получаем словарь дополнительных метаданных из метаданных модели...
                            var values =
                                metadata_property.GetValue(model_metadata, null) as
                                Dictionary<string, object>;


                            if (values != null)
                            {
                                // обходим...
                                foreach (var key_value_pair in additional_val)
                                {
                                    // заносим в словарь.
                                    values.Add(key_value_pair.Key, key_value_pair.Value);
                                }
                            }
                        }
                    }
                }
            }
            return model_metadata;
        }

        #endregion
    }
}