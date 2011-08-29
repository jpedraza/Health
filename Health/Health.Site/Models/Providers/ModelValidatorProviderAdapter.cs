using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Site.Models.Configuration;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Провайдер валидации моделей.
    /// </summary>
    public class ModelValidatorProviderAdapter : AssociatedValidatorProvider
    {
        /// <summary>
        /// Биндер.
        /// </summary>
        protected ModelMetadataProviderBinder Binder { get; set; }

        public ModelValidatorProviderAdapter(ModelMetadataProviderBinder binder)
        {
            Binder = binder;
        }

        #region Overrides of AssociatedValidatorProvider

        /// <summary>
        /// Получает средства проверки для модели, используя метаданные, контекст контроллера и список атрибутов.
        /// </summary>
        /// <returns>
        /// Средства проверки для модели.
        /// </returns>
        /// <param name="metadata">Метаданные.</param><param name="context">Контекст контроллера.</param><param name="attributes">Список атрибутов.</param>
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            if (Binder.IsHaveConfiguration(metadata.ContainerType))
            {
                var config = Binder.ResolveConfiguration(metadata.ContainerType).GetMetadata(metadata.ContainerType,
                                                                                             metadata.PropertyName);
                if (config != null && config.ValidatorRule != null && config.ValidatorRule.Count > 0)
                {
                    var model_validator =
                        new RangeValidatorRule().Create(
                            config.ValidatorRule[0],
                            metadata, context);
                    if (model_validator != null)
                    {
                        yield return model_validator;
                    }
                }
            }
            yield break;   
        }

        #endregion
    }
}