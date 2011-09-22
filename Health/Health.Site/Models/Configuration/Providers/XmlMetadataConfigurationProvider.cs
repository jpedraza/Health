using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Health.Core.API;

namespace Health.Site.Models.Configuration.Providers
{
    /// <summary>
    /// Xml провайдер конфигурации для модели.
    /// </summary>
    public class XmlMetadataConfigurationProvider : MetadataConfigurationProvider
    {
        /// <summary>
        /// Путь поиска файлов с метаданными.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="di_kernel"></param>
        /// <param name="path">Путь поиска файлов с метаданными.</param>
        public XmlMetadataConfigurationProvider(IDIKernel di_kernel, string path) : base(di_kernel)
        {
            _path = path;
        }

        #region Implementation of MetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="container_type"></param>
        /// <param name="model_accessor"></param>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Результат.</returns>
        public override bool IsHaveMetadata(Type container_type, Func<object> model_accessor, Type model_type,
                                   string property_name, params object[] parameters)
        {
            if (model_type == null) return false;
            ModelMetadataConfiguration model_metadata = ParseXml(model_type);
            IDictionary<string, ModelMetadataPropertyConfiguration> properties = model_metadata.Properties;
            foreach (var property in properties)
            {
                if (property.Key == property_name) return true;
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
        /// <param name="parameters">Дополнительные параметры.</param>
        /// <returns>Метаданные для свойства.</returns>
        public override ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor,
                                                              Type model_type, string property_name,
                                                              params object[] parameters)
        {
            if (model_type == null) return null;
            ModelMetadataConfiguration model_metadata = ParseXml(model_type);
            IDictionary<string, ModelMetadataPropertyConfiguration> properties = model_metadata.Properties;
            foreach (var property in properties)
            {
                if (property.Key == property_name) return property.Value;
            }
            return null;
        }

        /// <summary>
        /// Парсинг xml-файла с метаданными.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Конфигурация метаданных для модели.</returns>
        public ModelMetadataConfiguration ParseXml(Type model_type)
        {
            string file = String.Format("{0}{1}.Model.xml", _path, model_type.Name);
            var model_metadata = new ModelMetadataConfiguration
                                     {
                                         Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>()
                                     };
            if (!File.Exists(file)) return model_metadata;
            var xml_document = new XmlDocument();
            xml_document.Load(file);
            XmlNodeList model_node_list = xml_document.SelectNodes("/model");
            if (model_node_list == null || model_node_list.Count == 0) return model_metadata;
            Dictionary<string, ModelMetadataPropertyConfiguration> properties = XmlGetProperties(model_node_list[0],
                                                                                                 model_type);
            model_metadata.Properties = properties;
            return model_metadata;
        }

        /// <summary>
        /// Парсинг конфигурации свойств модели.
        /// </summary>
        /// <param name="model_node">Xml-узел модели.</param>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Словарь конйигурации свойств модели.</returns>
        private Dictionary<string, ModelMetadataPropertyConfiguration> XmlGetProperties(XmlNode model_node,
                                                                                        Type model_type)
        {
            var properties = new Dictionary<string, ModelMetadataPropertyConfiguration>();
            XmlNodeList properties_node_list = model_node.SelectNodes("property");
            if (properties_node_list == null || properties_node_list.Count == 0) return properties;
            foreach (XmlNode property_node in properties_node_list)
            {
                XmlAttributeCollection attribute_property_node_list = property_node.Attributes;
                if (attribute_property_node_list == null
                    || attribute_property_node_list.Count != 1
                    || attribute_property_node_list[0].Name != "name") continue;
                string property_node_name = attribute_property_node_list["name"].Value;
                if (String.IsNullOrEmpty(property_node_name)) continue;
                PropertyInfo model_type_property = model_type.GetProperty(property_node_name);
                if (model_type_property == null) continue;
                ModelMetadataPropertyConfiguration property_configuration = XmlGetPropertyConfiguration(property_node);
                properties.Add(property_node_name, property_configuration);
            }
            return properties;
        }

        /// <summary>
        /// Получить конфигурацию свойств модели.
        /// </summary>
        /// <param name="property_node">Xml-узел свойства.</param>
        /// <returns>Конйигурация свойства модели.</returns>
        private ModelMetadataPropertyConfiguration XmlGetPropertyConfiguration(XmlNode property_node)
        {
            var model_metadata_property = new ModelMetadataPropertyConfiguration();
            XmlGetMetadata(property_node, ref model_metadata_property);
            XmlGetAttribute(property_node, ref model_metadata_property);
            return model_metadata_property;
        }

        /// <summary>
        /// Получить атрибуты свойства.
        /// </summary>
        /// <param name="property_node">Xml-узел свойства.</param>
        /// <param name="property_configuration">Конфигурация свойства.</param>
        private void XmlGetAttribute(XmlNode property_node,
                                     ref ModelMetadataPropertyConfiguration property_configuration)
        {
            property_configuration.Attributes = new List<Attribute>();
            XmlNodeList attribute_node_list = property_node.SelectNodes("attribute");
            if (attribute_node_list == null || attribute_node_list.Count == 0) return;
            foreach (XmlNode attribute_node in attribute_node_list)
            {
                string attribute_node_type_name = XmlGetNodeAttribute(attribute_node, "type");
                Type attribute_type = Type.GetType(attribute_node_type_name);
                if (attribute_type == null) continue;
                XmlNodeList attribute_param_node_list = attribute_node.SelectNodes("param");
                var param_list = new List<string>();
                if (attribute_param_node_list != null && attribute_param_node_list.Count != 0)
                {
                    foreach (XmlNode param_node in attribute_param_node_list)
                    {
                        string param_value = XmlGetNodeAttribute(param_node, "value");
                        param_list.Add(param_value);
                    }
                }
                ConstructorInfo constructor_info =
                    attribute_type.GetConstructors().Where(c => c.GetParameters().Count() == param_list.Count).
                        FirstOrDefault();
                if (constructor_info == null) continue;
                ParameterInfo[] contructor_params_info = constructor_info.GetParameters();
                var param_objects = new object[param_list.Count];
                for (int i = 0; i < param_list.Count; i++)
                {
                    param_objects[i] = Convert.ChangeType(param_list[i], contructor_params_info[i].ParameterType);
                }
                var attribute_instance = Activator.CreateInstance(attribute_type, param_objects) as Attribute;
                if (attribute_instance == null) continue;

                XmlNodeList attribute_property_node_list = attribute_node.SelectNodes("property");
                if (attribute_property_node_list == null || attribute_property_node_list.Count == 0) continue;
                foreach (XmlNode attribute_property_node in attribute_property_node_list)
                {
                    string property_node_name = XmlGetNodeAttribute(attribute_property_node, "name");
                    string property_node_value = XmlGetNodeAttribute(attribute_property_node, "value");

                    PropertyInfo property_info = attribute_type.GetProperty(property_node_name);
                    if (property_info == null) continue;
                    Type property_type = property_info.PropertyType;

                    object value = Convert.ChangeType(property_node_value, property_type);
                    if (value == null) continue;

                    property_info.SetValue(attribute_instance, value, null);
                }
                property_configuration.Attributes.Add(attribute_instance);
            }
        }

        /// <summary>
        /// Получить метаданные для свойства.
        /// </summary>
        /// <param name="property_node">Xml-узел свойства.</param>
        /// <param name="property_configuration">Конфигурация свойства.</param>
        private void XmlGetMetadata(XmlNode property_node, ref ModelMetadataPropertyConfiguration property_configuration)
        {
            // TODO: Добавить биндинг дополнительных атрибутов.
            XmlNodeList metadata_node_list = property_node.SelectNodes("metadata");
            if (metadata_node_list == null || metadata_node_list.Count == 0) return;
            foreach (XmlNode metadata_node in metadata_node_list)
            {
                string metadata_node_name = XmlGetNodeAttribute(metadata_node, "name");
                PropertyInfo model_metadata_property_info =
                    typeof (ModelMetadataPropertyConfiguration).GetProperty(metadata_node_name);
                if (model_metadata_property_info == null) continue;
                string metadata_node_value = XmlGetNodeAttribute(metadata_node, "value");
                object value = Convert.ChangeType(metadata_node_value, model_metadata_property_info.PropertyType);
                model_metadata_property_info.SetValue(property_configuration, value, null);
            }
        }

        /// <summary>
        /// Получить значение атрибута узла.
        /// </summary>
        /// <param name="node">Xml-узел.</param>
        /// <param name="attribute_name">Имя атрибута.</param>
        /// <returns>Значение атрибута.</returns>
        private string XmlGetNodeAttribute(XmlNode node, string attribute_name)
        {
            XmlAttributeCollection attribute_collection = node.Attributes;
            if (attribute_collection == null || attribute_collection.Count == 0) return String.Empty;
            foreach (XmlAttribute attribute in attribute_collection)
            {
                if (attribute.Name == attribute_name) return attribute.Value;
            }
            return String.Empty;
        }

        #endregion
    }
}