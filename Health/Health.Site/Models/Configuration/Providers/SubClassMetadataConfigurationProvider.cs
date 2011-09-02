using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration.Providers
{
    public class SubClassMetadataConfigurationProvider : IMetadataConfigurationProvider
    {
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
            if (model_type == typeof(Patient))
            {
                return true;
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
            if (model_type == typeof(Patient))
            {
                var meta = new ModelMetadataConfiguration
                {
                    Properties = new Dictionary<string, ModelMetadataPropertyConfiguration>
                                                    {
                                                        {
                                                            "Card", new ModelMetadataPropertyConfiguration
                                                                        {
                                                                            DisplayName = "some name for card",
                                                                            ShowForEdit = true,
                                                                            //IsRequired = true,
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
                                                                                                ErrorMessage =
                                                                                                    "Значение должно умещаться в диапазон."
                                                                                            }
                                                                                    }
                                                                        }
                                                            }
                                                    }
                };
                return meta.Properties.ContainsKey(property_name) ? meta.Properties[property_name] : null;
            }
            return null;
        }

        #endregion
    }
}