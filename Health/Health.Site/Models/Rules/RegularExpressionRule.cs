using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Models.Rules
{
    [Serializable]
    public class RegularExpressionValidatorConfig : IValidatorRuleConfig
    {
        public string Pattern { get; set; }

        #region Implementation of IValidatorRuleConfig

        public string ErrorMessage { get; set; }

        #endregion
    }

    public class RegularExpressionValidatorRule : ModelValidatorRule, IModelValidatorRule
    {
        #region Implementation of IModelValidatorRule<in RegularExpressionValidatorConfig>

        public ModelValidator Create(IValidatorRuleConfig rule_config, ModelMetadata model_metadata, ControllerContext controller_context)
        {
            if (rule_config is RegularExpressionValidatorConfig)
            {
                var config = rule_config as RegularExpressionValidatorConfig;
                var attribute = new RegularExpressionAttribute(config.Pattern)
                                    {
                                        ErrorMessage = config.ErrorMessage
                                    };
                var adapter = new RegularExpressionAttributeAdapter(model_metadata, controller_context, attribute);
                return adapter;
            }
            throw GenerateConfigException(GetType(), typeof (RegularExpressionValidatorConfig), rule_config.GetType());
        }

        #endregion
    }
}