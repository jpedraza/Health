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
        #region Implementation of IMetadataConfigurationProvider

        /// <summary>
        /// Существуют ли метаданные для свойства модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Свойство.</param>
        /// <returns>Результат.</returns>
        public bool IsHaveMetadata(Type model_type, string property_name)
        {
            return model_type == typeof (TestModel);
        }

        /// <summary>
        /// Получить метаданные для свойства модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <param name="property_name">Имя свойства.</param>
        /// <returns>Метаданные для свойства.</returns>
        public ModelMetadataPropertyConfiguration GetMetadata(Type model_type, string property_name)
        {
            string metadata_model_type_name = String.Format("Health.Site.Models.Metadata.{0}Metadata, Health.Site",
                                                       model_type.Name);
            Type metadata_model_type = Type.GetType(metadata_model_type_name);
            var model_metadata = new ModelMetadataPropertyConfiguration();
            if (metadata_model_type == null)
            {
                return model_metadata;
            }
            PropertyInfo[] properties = metadata_model_type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != property_name)
                {
                    continue;
                }
                IList<Attribute> custom_attributes = new BindingList<Attribute>();
                object[] attributes = property.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    custom_attributes.Add(attribute as Attribute);
                }
                model_metadata.Attributes = custom_attributes;
            }
            return model_metadata;
        }

        #endregion
    }
}