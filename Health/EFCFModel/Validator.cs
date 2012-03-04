using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace EFCFModel
{
    public class ValidationResult : System.ComponentModel.DataAnnotations.ValidationResult
    {
        public PropertyDescriptor Descriptor { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/>, используя указанное сообщение об ошибке.
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        public ValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:EFCFModel.ValidationResult"/> с использованием указанного сообщения об ошибке и информацией о свойстве, непрошедшем проверку.
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        /// <param name="descriptor">Свойство, непрошедшее проверку.</param>
        public ValidationResult(string errorMessage, PropertyDescriptor descriptor)
            : base(errorMessage, new[] { descriptor.Name })
        {
            Descriptor = descriptor;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> с использованием указанного сообщения об ошибке и списка членов, имеющих ошибки проверки.
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        /// <param name="memberNames">Список членов, имена которых вызвали ошибки проверки.</param>
        public ValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> с помощью объекта <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/>.
        /// </summary>
        /// <param name="validationResult">Объект результата проверки.</param>
        protected ValidationResult(System.ComponentModel.DataAnnotations.ValidationResult validationResult) : base(validationResult)
        {
        }
    }

    public class Validator
    {
        public IEnumerable<ValidationResult> Errors { get; private set; }

        public IEnumerable<ValidationResult> Validate(object component)
        {
            IEnumerable<PropertyDescriptor> properties = TypeDescriptor.GetProperties(component).Cast<PropertyDescriptor>();
            foreach (PropertyDescriptor property in properties)
            {
                IEnumerable<ValidationAttribute> attributes = property.Attributes.OfType<ValidationAttribute>();
                foreach (ValidationAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(component)))
                        yield return new ValidationResult(
                            attribute.ErrorMessage ??
                            string.Format(CultureInfo.CurrentUICulture, "{0} validation failed.",
                                          attribute.GetType().Name), property);
                }
            }
        }

        public ValidationResult ValidateProperty(object component, PropertyDescriptor descriptor)
        {
            IEnumerable<ValidationAttribute> attributes = descriptor.Attributes.OfType<ValidationAttribute>();
            foreach (ValidationAttribute attribute in attributes)
            {
                if (!attribute.IsValid(descriptor.GetValue(component)))
                    return
                        new ValidationResult(
                            attribute.ErrorMessage ?? string.Format(CultureInfo.CurrentUICulture, "{0} validation failed.", attribute.GetType().Name), descriptor);
            }
            return null;
        }

        public bool IsValid(object component)
        {
            Errors = Validate(component);
            return !Errors.Any();
        }
    }
}
