using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Health.Core.Entities.POCO;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration
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
            if (model_type == typeof (TestModel))
            {
                return true;
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
            if (model_type == typeof (TestModel))
            {
                var meta = new ModelMetadataConfiguration
                               {
                                   Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>
                                                    {
                                                        {
                                                            "Name", new ModelMetadataPropertyConfiguration
                                                                        {
                                                                            DisplayName = "some name",
                                                                            ShowForDisplay = true,
                                                                            ShowForEdit = true,
                                                                            IsRequired = true,
                                                                            DataTypeName =
                                                                                DataType.EmailAddress.ToString(),
                                                                            AdditionalValues =
                                                                                new Dictionary<string, object>
                                                                                    {
                                                                                        {"some", 0}
                                                                                    },
                                                                            ValidatorRule =
                                                                                new List<IValidatorRuleConfig>
                                                                                    {
                                                                                        new RangeValidatorConfig
                                                                                            {
                                                                                                Min = 0,
                                                                                                Max = 100,
                                                                                                Message =
                                                                                                    "Значение должно умещаться в диапазон."
                                                                                            }
                                                                                    }
                                                                        }
                                                            },
                                                            {"Patient", new ModelMetadataPropertyConfiguration
                                                                            {
                                                                                Type = typeof(Patient)
                                                                            }}
                                                    }
                               };
                return meta.Properties.ContainsKey(property_name) ? meta.Properties[property_name] : null;
            }
            return null;
        }

        #endregion
    }
}