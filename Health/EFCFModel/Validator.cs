using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace EFCFModel
{
    public class Validator
    {
        public static IEnumerable<ValidationResult> Validate(object component)
        {
            return from descriptor in TypeDescriptor.GetProperties(component).Cast<PropertyDescriptor>()
                   from validation in descriptor.Attributes.OfType<ValidationAttribute>()
                   where !validation.IsValid(descriptor.GetValue(component))
                   select new ValidationResult(
                       validation.ErrorMessage ?? string.Format(CultureInfo.CurrentUICulture, "{0} validation failed.", validation.GetType().Name),
                       new[] { descriptor.Name });
        }
    }
}
