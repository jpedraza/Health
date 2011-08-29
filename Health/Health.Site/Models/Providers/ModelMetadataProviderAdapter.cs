using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using Health.Site.Models.Configuration;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Адаптер для привязки метаданных модели из провайдера конфигурации.
    /// </summary>
    public class ModelMetadataProviderAdapter : AssociatedMetadataProvider
    {
        /// <summary>
        /// Провайдер конйигурации.
        /// </summary>
        protected IMetadataConfigurationProvider ConfigurationProvider { get; set; }

        /// <summary>
        /// Биндер.
        /// </summary>
        protected ModelMetadataProviderBinder Binder { get; set; }

        public ModelMetadataProviderAdapter(IMetadataConfigurationProvider configuration_provider, ModelMetadataProviderBinder binder)
        {
            ConfigurationProvider = configuration_provider;
            Binder = binder;
        }

        #region Overrides of AssociatedMetadataProvider

        /// <summary>
        /// Создать метаданные для свойства модели.
        /// </summary>
        /// <param name="attributes">Атрибуты свойства.</param>
        /// <param name="container_type">Тип контейнера.</param>
        /// <param name="model_accessor"></param>
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

            // если в поставщике метаданных есть метаданные для типа модели с заданным свойством...
            if (ConfigurationProvider.IsHaveMetadata(container_type, property_name))
            {
                // получить метаданные для свойства...
                ModelMetadataPropertyConfiguration meta = ConfigurationProvider.GetMetadata(container_type, property_name);

                // если есть метаданные...
                if (meta != null)
                {
                    // если метаданные не имеют родительского типа...
                    if (meta.Type == null)
                    {
                        // получаем все свойства конйигурационной модели...
                        PropertyInfo[] property_infos = typeof (ModelMetadataPropertyConfiguration).GetProperties();

                        // обходим свойства...
                        foreach (PropertyInfo property_info in property_infos)
                        {
                            // получаем соответствующее свойство из класса метаданных модели...
                            var metadata_property = typeof (ModelMetadata).GetProperty(property_info.Name);

                            if (metadata_property != null)
                            {
                                // если это не словарь дополнительных параметров...
                                if (property_info.PropertyType != typeof (Dictionary<string, object>))
                                {
                                    // получаем значение свойства из провайдера конфигурации...
                                    object value = Convert.ChangeType(
                                        typeof (ModelMetadataPropertyConfiguration).GetProperty(property_info.Name).
                                            GetValue
                                            (meta, null),
                                        metadata_property.PropertyType);

                                    // заносим значение в иетаданные модели.
                                    metadata_property.SetValue(model_metadata, value, null);
                                }
                                    // в ином случае...
                                else
                                {
                                    // получаем словарь дополнительных метаданных из провайдера конфигурации...
                                    var additional_val =
                                        typeof (ModelMetadataPropertyConfiguration).GetProperty(property_info.Name).
                                            GetValue
                                            (meta, null)
                                        as
                                        Dictionary<string, object>;

                                    if (additional_val != null)
                                    {
                                        // получаем словарь дополнительных метаданных из метаданных модели...
                                        var values =
                                            metadata_property.GetValue(model_metadata, null) as
                                            Dictionary<string, object>;

                                        // обходим...
                                        foreach (KeyValuePair<string, object> key_value_pair in additional_val)
                                        {
                                            // заносим в словарь.
                                            if (values != null)
                                                values.Add(key_value_pair.Key, key_value_pair.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // если метаданные имеют родительский тип...
                    else
                    {
                        // создаем провайдер потипу модели...
                        AssociatedMetadataProvider provider = Binder.ResolveProvider(meta.Type) ?? this;
                        // создаем метаданные...
                        model_metadata = new ModelMetadata(provider, container_type, null, meta.Type, property_name);    
                    }
                }
            }
            return model_metadata;
        }

        #endregion
    }
}