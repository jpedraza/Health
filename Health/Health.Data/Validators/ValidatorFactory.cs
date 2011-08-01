using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.API;
using Health.API.Entities;

namespace Health.Data.Validators
{
    public class ValidatorFactory : IValidatorFactory
    {
        public string Message { get; protected set; }

        public bool IsValid(string validator_type, object value)
        {
            var validator = (IValueValidator)Activator.CreateInstance(Type.GetType(validator_type));
            bool result = validator.IsValid(value);
            if (!result)
            {
                Message = validator.Message;
            }
            return result;
        }
    }
}
