using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Models.Rules
{
    [Serializable]
    public class StringLengthValidatorConfig : IValidatorRuleConfig
    {
        public int MaximumLength { get; set; }

        #region Implementation of IValidatorRuleConfig

        public string ErrorMessage { get; set; }

        #endregion
    }

    public class StringLengthValidatorRule : ModelValidatorRule, IModelValidatorRule
    {
        #region Implementation of IModelValidatorRule

        public ModelValidator Create(IValidatorRuleConfig rule_config, ModelMetadata model_metadata, ControllerContext controller_context)
        {
            if (rule_config is StringLengthValidatorConfig)
            {
                var config = rule_config as StringLengthValidatorConfig;
                var attribute = new StringLengthAttribute(config.MaximumLength)
                                    {
                                        ErrorMessage = config.ErrorMessage
                                    };
                var adapter = new StringLengthAttributeAdapter(model_metadata, controller_context, attribute);
                return adapter;
            }
            throw GenerateConfigException(GetType(), typeof (StringLengthValidatorConfig), rule_config.GetType());
        }

        #endregion
    }
}