using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Health.Site.Models.Rules
{
    public interface IModelValidatorRule
    {
        ModelValidator Create(IValidatorRuleConfig config, ModelMetadata model_metadata, ControllerContext controller_context);
    }
}
