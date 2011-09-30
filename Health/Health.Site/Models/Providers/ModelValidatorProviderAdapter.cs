using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.TypeProvider;
using Health.Core.Entities.Virtual;
using Health.Site.Controllers;
using Health.Site.Models.Configuration;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Провайдер валидации моделей.
    /// </summary>
    public class ModelValidatorProviderAdapter : ModelValidatorProvider 
    {
        /// <summary>
        /// Биндер.
        /// </summary>
        protected readonly ModelMetadataProviderBinder Binder;

        protected readonly IDIKernel DIKernel;

        protected readonly ModelMetadataProviderManager MetadataProviderManager;

        public ModelValidatorProviderAdapter(IDIKernel diKernel)
        {
            DIKernel = diKernel;
            Binder = diKernel.Get<ModelMetadataProviderBinder>();
            MetadataProviderManager = diKernel.Get<ModelMetadataProviderManager>();
        }

        private static Attribute ObjectTypeToAttributeType(object o)
        {
            return o as Attribute;
        }

        #region Overrides of ModelValidatorProvider

        /// <summary>
        /// Получает список средств проверки.
        /// </summary>
        /// <returns>
        /// Список средств проверки.
        /// </returns>
        /// <param name="metadata">Метаданные.</param><param name="context">Контекст.</param>
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata != null && metadata.ContainerType != null && !String.IsNullOrEmpty(metadata.PropertyName))
            {
                var repository = DIKernel.Get<DynamicMetadataRepository>();
                Type metadataType = repository.GetMetadataType(metadata.ContainerType);
                if (metadataType != null)
                {
                    PropertyInfo propertyInfo = metadataType.GetProperty(metadata.PropertyName);
                    if (propertyInfo != null)
                    {
                        object[] newAttributesTemp = propertyInfo.GetCustomAttributes(false);
                        Attribute[] newAttributes = Array.ConvertAll(newAttributesTemp, ObjectTypeToAttributeType);
                        foreach (var attribute in newAttributes)
                        {
                            var att = attribute as ValidationAttribute;
                            if (att != null)
                                yield return new DataAnnotationsModelValidator(metadata, context, att);
                        }
                    }
                }
            }
            yield break;
        }

        #endregion
    }
}