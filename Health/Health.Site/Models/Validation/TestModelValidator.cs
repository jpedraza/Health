using System;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models.Validators
{
    public static class TestModelValidator
    {
        public static ValidationResult ContainsA(string name, ValidationContext validation_context)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new ValidationResult("Имя должно содержать символ '1'.");
            }
            return name.Contains("1")
                       ? ValidationResult.Success
                       : new ValidationResult("Имя должно содержать символ '1'.");
        }
    }
}