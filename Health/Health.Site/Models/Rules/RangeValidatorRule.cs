using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.Site.Models.Rules
{
    public class RangeValidatorConfig : IValidatorRuleConfig
    {
        public double Min { get; set; }

        public double Max { get; set; }

        public string Message { get; set; }
    }

    public class RangeValidatorRule : IModelValidatorRule
    {
        #region Implementation of IModelValidatorRule

        public ModelValidator Create(IValidatorRuleConfig rule_config, ModelMetadata model_metadata, ControllerContext controller_context)
        {
            var config = rule_config as RangeValidatorConfig;
            if (config != null)
            {
                var attribute = new RangeAttribute(config.Min, config.Max)
                                    {
                                        ErrorMessage = config.Message
                                    };
                var adapter = new RangeAttributeAdapter(model_metadata, controller_context, attribute);
                return adapter;
            }
            throw new Exception("Неверная конфигурация источника для RangeValidatorRule.");
        }

        #endregion
    }
}