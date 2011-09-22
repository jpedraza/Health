using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Controllers;
using Health.Site.Models.Configuration;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Провайдер валидации моделей.
    /// </summary>
    public class ModelValidatorProviderAdapter : DataAnnotationsModelValidatorProvider
    {
        /// <summary>
        /// Биндер.
        /// </summary>
        protected readonly ModelMetadataProviderBinder Binder;

        protected readonly IDIKernel DIKernel;

        protected readonly ModelMetadataProviderManager MetadataProviderManager;

        public ModelValidatorProviderAdapter(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
            Binder = di_kernel.Get<ModelMetadataProviderBinder>();
            MetadataProviderManager = di_kernel.Get<ModelMetadataProviderManager>();
        }

        #region Overrides of AssociatedValidatorProvider

        /// <summary>
        /// Получает средства проверки для модели, используя метаданные, контекст контроллера и список атрибутов.
        /// </summary>
        /// <returns>
        /// Средства проверки для модели.
        /// </returns>
        /// <param name="metadata">Метаданные.</param><param name="context">Контекст контроллера.</param><param name="attributes">Список атрибутов.</param>
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context,
                                                                     IEnumerable<Attribute> attributes)
        {
            if (context.Controller.GetType() != typeof(ErrorController) &&
                Binder.IsHaveConfiguration(metadata.ContainerType))
            {
                MetadataConfigurationProvider configuration_provider =
                    Binder.ResolveConfiguration(metadata.ContainerType);

                ModelMetadataPropertyConfiguration config =
                    configuration_provider.GetMetadata(metadata.ContainerType, null, metadata.ContainerType,
                                                       metadata.PropertyName);

                if (config != null && config.Attributes != null && config.Attributes.Count > 0)
                {
                    IList<Attribute> attributes_list = config.Attributes;
                    IEnumerable<ModelValidator> new_validators = base.GetValidators(metadata, context, attributes_list);
                    foreach (ModelValidator new_validator in new_validators)
                    {
                        yield return new_validator;
                    }
                }
            }
            yield break;
        }        

        #endregion
    }
}