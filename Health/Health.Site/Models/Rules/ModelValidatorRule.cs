using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.Site.Models.Rules
{
    public class ModelValidatorRule 
    {
        public Exception GenerateConfigException(Type rule_type, Type required_config_type, Type config_type)
        {
            return
                new Exception(String.Format(
                    "Неверный конфиг для правила валидации [{0}]. Ожидалось - [{1}], получили - [{2}]", rule_type.FullName,
                    required_config_type.FullName, config_type.FullName));
        }
    }
}