using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration.Providers
{
    public class XmlMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
        /// <summary>
        /// Путь по которому ищутся конфигурации метаданных для моделей.
        /// Формат поиска: String.Format("{0}{1}.Model.xml", _path, model_type.Name);
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="path">
        /// Путь по которому ищутся конфигурации метаданных для моделей.
        /// Формат поиска: String.Format("{0}{1}.Model.xml", _path, model_type.Name);
        /// </param>
        public XmlMetadataConfigurationProvider(string path)
        {
            _path = path;
            ConfigurationsCache = new Dictionary<Type, ModelMetadataConfiguration>();
        }

        /// <summary>
        /// Кэш-конфигураций моделей.
        /// </summary>
        private IDictionary<Type, ModelMetadataConfiguration> ConfigurationsCache { get; set; }

        #region Implementation of IMetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <returns>Результат.</returns>
        public bool IsHaveMetadata(Type model_type, string property_name)
        {
            if (model_type != null)
            {
                ModelMetadataConfiguration configuration = GetModelMetadata(model_type);
                Dictionary<string, ModelMetadataPropertyConfiguration> property_configurations =
                    configuration.Properties;
                foreach (var property_configuration in property_configurations)
                {
                    if (property_configuration.Key == property_name)
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
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства.</returns>
        public ModelMetadataPropertyConfiguration GetMetadata(Type model_type, string property_name)
        {
            if (model_type != null)
            {
                ModelMetadataConfiguration configuration = GetModelMetadata(model_type);
                Dictionary<string, ModelMetadataPropertyConfiguration> property_configurations =
                    configuration.Properties;
                foreach (var property_configuration in property_configurations)
                {
                    if (property_configuration.Key == property_name)
                    {
                        return property_configuration.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Парсим xml.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Метаданные для модели.</returns>
        private ModelMetadataConfiguration ParseXml(Type model_type)
        {
            var xml_document = new XmlDocument();
            var model_metadata = new ModelMetadataConfiguration
                                     {Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>()};
            string file = String.Format("{0}{1}.Model.xml", _path, model_type.Name);
            if (!File.Exists(file))
            {
                return model_metadata;
            }
            xml_document.Load(file);
            // получаем корневой элемент model...
            XmlNodeList node_model_list = xml_document.SelectNodes("/model");
            if (node_model_list == null || node_model_list.Count != 1)
            {
                return model_metadata;
            }
            XmlNode node_model = node_model_list[0];

            // получаем конфигурацию свойств модели...
            XmlNodeList node_property_list = node_model.SelectNodes("property");
            if (node_property_list == null)
            {
                return model_metadata;
            }
            // обходи каждое свойство...
            foreach (XmlNode node_property in node_property_list)
            {
                if (node_property.Attributes == null)
                {
                    continue;
                }
                var metadata_property = new ModelMetadataPropertyConfiguration
                                            {ValidatorRule = new List<IValidatorRuleConfig>()};

                // получаем имя свойства...
                string key_name = node_property.Attributes["name"].Value;
                // получаем метаданные свойства...
                XmlNodeList node_metadata_list = node_property.SelectNodes("metadata");
                if (node_metadata_list == null)
                {
                    continue;
                }
                // обходим метаданные...
                foreach (XmlNode node_metadata in node_metadata_list)
                {
                    XmlAttributeCollection attribute_collection = node_metadata.Attributes;
                    string name = String.Empty;
                    string value = String.Empty;
                    if (attribute_collection == null)
                    {
                        continue;
                    }
                    foreach (XmlAttribute metadata_attribute in attribute_collection)
                    {
                        if (metadata_attribute.Name == "name")
                        {
                            name = metadata_attribute.Value;
                        }
                        if (metadata_attribute.Name == "value")
                        {
                            value = metadata_attribute.Value;
                        }
                    }
                    if (String.IsNullOrEmpty(name))
                    {
                        continue;
                    }
                    PropertyInfo property_info = typeof (ModelMetadataPropertyConfiguration).GetProperty(name);
                    if (property_info != null)
                    {
                        object converted_value =
                            property_info.PropertyType == typeof(Type) ?
                            Type.GetType(value) : Convert.ChangeType(value, property_info.PropertyType);
                        property_info.SetValue(metadata_property, converted_value, null);
                    }
                }
                model_metadata.Properties.Add(key_name, metadata_property);

                // получаем правила проверки свойства...
                XmlNodeList node_rule_list = node_property.SelectNodes("rule");
                if (node_rule_list == null)
                {
                    continue;
                }
                // обходим правила...
                foreach (XmlNode node_rule in node_rule_list)
                {
                    if (node_rule.Attributes == null)
                    {
                        continue;
                    }
                    // получаем тип конфигурации для свойства...
                    string type_name = node_rule.Attributes["type"].Value;
                    if (String.IsNullOrEmpty(type_name))
                    {
                        continue;
                    }
                    Type rule_type = Type.GetType(type_name);
                    if (rule_type == null)
                    {
                        continue;
                    }

                    // инициализируем экземпляр конфигурации...
                    object rule_config_instance = Activator.CreateInstance(rule_type);
                    if (rule_config_instance is IValidatorRuleConfig)
                    {
                        // получаем список параметров необходимых для инициализации конфигурации...
                        XmlNodeList node_config_rule = node_rule.SelectNodes("param");
                        if (node_config_rule == null)
                        {
                            continue;
                        }
                        foreach (XmlNode node_rule_config in node_config_rule)
                        {
                            XmlAttributeCollection attribute_collection = node_rule_config.Attributes;
                            string name = String.Empty;
                            string value = String.Empty;
                            if (attribute_collection == null)
                            {
                                continue;
                            }
                            foreach (XmlAttribute attribute in attribute_collection)
                            {
                                if (attribute.Name == "name")
                                {
                                    name = attribute.Value;
                                }
                                if (attribute.Name == "value")
                                {
                                    value = attribute.Value;
                                }
                            }
                            if (!String.IsNullOrEmpty(name) && value != null)
                            {
                                PropertyInfo property = rule_type.GetProperty(name);
                                if (property != null)
                                {
                                    object converted_value = 
                                        property.PropertyType == typeof (Type) ? 
                                        Type.GetType(value) : Convert.ChangeType(value, property.PropertyType);
                                    property.SetValue(rule_config_instance, converted_value, null);
                                }
                            }
                        }
                        metadata_property.ValidatorRule.Add((IValidatorRuleConfig) rule_config_instance);
                    }
                }
            }
            return model_metadata;
        }

        /// <summary>
        /// Возвращает метаданные для заданного типа модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Метаданные.</returns>
        private ModelMetadataConfiguration GetModelMetadata(Type model_type)
        {
            // обходим кэш-конфигураций...
            foreach (var configuration in ConfigurationsCache)
            {
                // если для заданного типа модели уже есть конфигурация...
                if (configuration.Key == model_type)
                {
                    // возвращаем ее.
                    return configuration.Value;
                }
            }
            // в противном случае получаем конфигурацию из источника...
            ModelMetadataConfiguration model_metadata_configuration = ParseXml(model_type);
            AddModelConfigurationToCache(model_type, model_metadata_configuration);
            return model_metadata_configuration;
        }

        private void AddModelConfigurationToCache(Type model_type,
                                                  ModelMetadataConfiguration model_metadata_configuration)
        {
            ConfigurationsCache.Add(model_type, model_metadata_configuration);
        }

        #endregion
    }
}