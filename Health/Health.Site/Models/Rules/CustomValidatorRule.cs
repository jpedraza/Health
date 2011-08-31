using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Models.Rules
{
    [Serializable]
    public class CustomValidatorConfig : IValidatorRuleConfig
    {
        public Type ClassType { get; set; }

        public string MethodName { get; set; }

        #region Implementation of IValidatorRuleConfig

        public string ErrorMessage { get; set; }

        #endregion
    }

    public class CustomValidatorRule : ModelValidatorRule, IModelValidatorRule
    {
        #region Implementation of IModelValidatorRule

        public ModelValidator Create(IValidatorRuleConfig rule_config, ModelMetadata model_metadata, ControllerContext controller_context)
        {
            if (rule_config is CustomValidatorConfig)
            {
                var config = rule_config as CustomValidatorConfig;
                var attribute = new CustomValidationAttribute(config.ClassType, config.MethodName)
                                    {
                                        ErrorMessage = config.ErrorMessage
                                    };
                return new DataAnnotationsModelValidator<CustomValidationAttribute>(model_metadata, controller_context,
                                                                                    attribute);
            }
            throw GenerateConfigException(GetType(), typeof (CustomValidatorConfig), rule_config.GetType());
        }

        #endregion
    }
}