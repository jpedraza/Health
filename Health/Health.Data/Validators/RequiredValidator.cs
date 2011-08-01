using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.API.Entities;

namespace Health.Data.Validators
{
    public class RequiredValidator : IValueValidator
    {
        public string Message { get; protected set; }

        public bool IsValid(object value)
        {
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            Message = "Значение не может быть пустым";
            return false;
        }
    }
}
