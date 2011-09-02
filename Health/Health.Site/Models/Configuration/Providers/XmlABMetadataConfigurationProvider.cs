using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Health.Site.Models.Configuration.Providers
{
    public class XmlABMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
        private readonly string _path;

        public XmlABMetadataConfigurationProvider(string path)
        {
            _path = path;
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
        /// <returns>Метаданные для свойства.</returns>
        public ModelMetadataPropertyConfiguration GetMetadata(Type container_type, Func<object> model_accessor, Type model_type, string property_name)
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

        public ModelMetadataConfiguration ParseXml(Type model_type)
        {
            string file = String.Format("{0}{1}.Model.AB.xml", _path, model_type.Name);
            var model_metadata = new ModelMetadataConfiguration
                                     {
                                         Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>()
                                     };
            if (!File.Exists(file)) return model_metadata;
            var xml_document = new XmlDocument();
            xml_document.Load(file);
            XmlNodeList model_node_list = xml_document.SelectNodes("/model");
            if (model_node_list == null || model_node_list.Count == 0) return model_metadata;
            Dictionary<string, ModelMetadataPropertyConfiguration> properties = XmlGetProperties(model_node_list[0], model_type);
            model_metadata.Properties = properties;
            return model_metadata;
        }

        private Dictionary<string, ModelMetadataPropertyConfiguration> XmlGetProperties(XmlNode model_node, Type model_type)
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

        private ModelMetadataPropertyConfiguration XmlGetPropertyConfiguration(XmlNode property_node)
        {
            var model_metadata_property = new ModelMetadataPropertyConfiguration();
            XmlGetMetadata(property_node, ref model_metadata_property);
            XmlGetAttribute(property_node, ref model_metadata_property);
            return model_metadata_property;
        }

        private void XmlGetAttribute(XmlNode property_node, ref ModelMetadataPropertyConfiguration property_configuration)
        {
            //TODO: Добавить биндинг свойств для атрибутов.
            property_configuration.Attributes = new List<Attribute>();
            XmlNodeList attribute_node_list = property_node.SelectNodes("attribute");
            if (attribute_node_list == null || attribute_node_list.Count == 0) return;
            foreach (XmlNode attribute_node in attribute_node_list)
            {
                string attribute_node_type_name = XmlGetNodeAttribute(attribute_node, "type");
                Type attribute_type = Type.GetType(attribute_node_type_name);
                if (attribute_type == null) continue;
                XmlNodeList param_node_list = attribute_node.SelectNodes("param");
                if (param_node_list == null || param_node_list.Count == 0) continue;
                var param_list = new List<string>();
                foreach (XmlNode param_node in param_node_list)
                {
                    string param_value = XmlGetNodeAttribute(param_node, "value");
                    param_list.Add(param_value);
                }
                var types = new Type[param_list.Count];
                for (int i = 0; i < param_list.Count; i++)
                {
                    types[i] = typeof (object);
                }
                ConstructorInfo constructor_info = 
                    attribute_type.GetConstructors().Where(c => c.GetParameters().Count() == param_list.Count).FirstOrDefault();
                if (constructor_info == null) continue;
                ParameterInfo[] contructor_params_info = constructor_info.GetParameters();
                var param_objects = new object[param_list.Count];
                for (int i = 0; i < param_list.Count; i++)
                {
                    param_objects[i] = Convert.ChangeType(param_list[i], contructor_params_info[i].ParameterType);
                }
                var attribute_instance = Activator.CreateInstance(attribute_type, param_objects) as Attribute;
                if (attribute_instance == null) continue;
                property_configuration.Attributes.Add(attribute_instance);
            }
        }

        private void XmlGetMetadata(XmlNode property_node, ref ModelMetadataPropertyConfiguration property_configuration)
        {
            // TODO: Добавить биндинг дополнительных атрибутов.
            XmlNodeList metadata_node_list = property_node.SelectNodes("metadata");
            if (metadata_node_list == null || metadata_node_list.Count == 0) return;
            foreach (XmlNode metadata_node in metadata_node_list)
            {
                string metadata_node_name = XmlGetNodeAttribute(metadata_node, "name");
                PropertyInfo model_metadata_property_info =
                    typeof(ModelMetadataPropertyConfiguration).GetProperty(metadata_node_name);
                if (model_metadata_property_info == null) continue;
                string metadata_node_value = XmlGetNodeAttribute(metadata_node, "value");
                object value = Convert.ChangeType(metadata_node_value, model_metadata_property_info.PropertyType);
                model_metadata_property_info.SetValue(property_configuration, value, null);
            }
        }

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