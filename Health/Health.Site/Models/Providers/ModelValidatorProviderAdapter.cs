using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;
using Health.Site.Controllers;
using Health.Site.Models.Rules;

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
            if (context.Controller.GetType() != typeof(ErrorController) && Binder.IsHaveConfiguration(metadata.ContainerType))
            {
                var config = Binder.ResolveConfiguration(metadata.ContainerType).GetMetadata(null, null, metadata.ContainerType, metadata.PropertyName);

                if (config != null && config.Attributes != null && config.Attributes.Count > 0)
                {
                    IList<Attribute> attributes_list = config.Attributes;
                    IEnumerable<ModelValidator> new_validators = base.GetValidators(metadata, context, attributes_list);
                    foreach (ModelValidator new_validator in new_validators)
                    {
                        yield return new_validator;
                    }
                    yield break;
                }

                if (config != null && config.ValidatorRule != null && config.ValidatorRule.Count > 0)
                {
                    IList<IValidatorRuleConfig> validator_rule_configs = config.ValidatorRule;
                    foreach (IValidatorRuleConfig rule_config in validator_rule_configs)
                    {
                        string rule_type = rule_config.GetType().Namespace + "." + rule_config.GetType().Name.Replace("ValidatorConfig", "ValidatorRule") + ", " + rule_config.GetType().Assembly;
                        Type validator_type = Type.GetType(rule_type);
                        if (validator_type != null)
                        {
                            var rule = (IModelValidatorRule)Activator.CreateInstance(validator_type);
                            ModelValidator model_validator = rule.Create(rule_config, metadata, context);
                            yield return model_validator;
                        }
                    }
                }
            }
            yield break;
        }

        #endregion
    }
}