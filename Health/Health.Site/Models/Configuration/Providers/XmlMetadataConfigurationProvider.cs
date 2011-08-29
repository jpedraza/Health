using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration.Providers
{
    public class XmlMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
        private readonly string _path;

        public XmlMetadataConfigurationProvider(string path)
        {
            _path = path;
        }

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
                ModelMetadataConfiguration configuration = ParseXml(model_type);
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
                ModelMetadataConfiguration configuration = ParseXml(model_type);
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

        private ModelMetadataConfiguration ParseXml(Type model_type)
        {
            var xml_document = new XmlDocument();
            var model_metadata = new ModelMetadataConfiguration
                                     {Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>()};
            xml_document.Load(_path + model_type.Name + ".Model.xml");
            XmlNodeList node_model = xml_document.SelectNodes("/model");
            if (node_model != null && node_model.Count == 1)
            {
                foreach (XmlNode xml_node in node_model)
                {
                    XmlNodeList node_property_list = xml_node.SelectNodes("property");
                    if (node_property_list != null)
                    {
                        foreach (XmlNode node_property in node_property_list)
                        {
                            if (node_property.Attributes != null)
                            {
                                var metadata_property = new ModelMetadataPropertyConfiguration
                                                            {ValidatorRule = new List<IValidatorRuleConfig>()};

                                string key_name = node_property.Attributes["name"].Value;
                                XmlNodeList node_metadata_list = node_property.SelectNodes("metadata");
                                if (node_metadata_list != null)
                                {
                                    foreach (XmlNode node_metadata in node_metadata_list)
                                    {
                                        XmlAttributeCollection attribute_collection = node_metadata.Attributes;
                                        string name = String.Empty;
                                        string value = String.Empty;
                                        if (attribute_collection != null)
                                        {
                                            foreach (XmlAttribute metadata_attribute in attribute_collection)
                                            {
                                                if (metadata_attribute.Name == "name")
                                                {
                                                    name = metadata_attribute.Value;
                                                }
                                                if (metadata_attribute.Name == "value")
                                                {
                                                    value = metadata_attribute
                                                        .Value;
                                                }
                                            }
                                            if (!String.IsNullOrEmpty(name))
                                            {
                                                PropertyInfo property_info =
                                                    typeof (ModelMetadataPropertyConfiguration).GetProperty(
                                                        name);
                                                if (property_info != null)
                                                {
                                                    object converted_value = Convert.ChangeType(value,
                                                                                                property_info.
                                                                                                    PropertyType);
                                                    property_info.SetValue(metadata_property, converted_value, null);
                                                }
                                            }
                                        }
                                    }
                                    model_metadata.Properties.Add(key_name, metadata_property);
                                }

                                XmlNodeList node_rule_list = node_property.SelectNodes("rule");
                                if (node_rule_list != null)
                                {
                                    foreach (XmlNode node_rule in node_rule_list)
                                    {
                                        if (node_rule.Attributes != null)
                                        {
                                            string type_name = node_rule.Attributes["type"].Value;
                                            if (!String.IsNullOrEmpty(type_name))
                                            {
                                                Type rule_type = Type.GetType(type_name);
                                                if (rule_type != null)
                                                {
                                                    object rule_config_instance = Activator.CreateInstance(rule_type);
                                                    if (rule_config_instance is IValidatorRuleConfig)
                                                    {
                                                        XmlNodeList node_config_rule = node_rule.SelectNodes("config");
                                                        if (node_config_rule != null)
                                                        {
                                                            foreach (XmlNode node_rule_config in node_config_rule)
                                                            {
                                                                XmlAttributeCollection attribute_collection =
                                                                    node_rule_config.Attributes;
                                                                if (attribute_collection != null)
                                                                {
                                                                    string name = String.Empty;
                                                                    object value = null;
                                                                    foreach (
                                                                        XmlAttribute attribute in attribute_collection)
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
                                                                        PropertyInfo property =
                                                                            rule_type.GetProperty(name);
                                                                        if (property != null)
                                                                        {
                                                                            object converted_value =
                                                                                Convert.ChangeType(value,
                                                                                                   property.PropertyType);
                                                                            property.SetValue(rule_config_instance,
                                                                                              converted_value, null);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            metadata_property.ValidatorRule.Add((IValidatorRuleConfig)rule_config_instance);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
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