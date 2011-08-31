using System.Web.Mvc;

namespace Health.Site.Models.Rules
{
    public interface IModelValidatorRule
    {
        ModelValidator Create(IValidatorRuleConfig rule_config, ModelMetadata model_metadata, ControllerContext controller_context);
    }
}
