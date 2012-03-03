using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace EFCFModel
{
    public class ValidationResult : System.ComponentModel.DataAnnotations.ValidationResult
    {
        public PropertyInfo PropertyInfo { get; private set; }

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
        /// <param name="propertyInfo">Свойство, непрошедшее проверку.</param>
        public ValidationResult(string errorMessage, PropertyInfo propertyInfo)
            : base(errorMessage, new[] { propertyInfo.Name })
        {
            PropertyInfo = propertyInfo;
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
            IEnumerable<PropertyInfo> properties =
                component.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                IEnumerable<ValidationAttribute> attributes = property.GetCustomAttributes(true).OfType<ValidationAttribute>();
                foreach (ValidationAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(component, null)))
                        yield return
                            new ValidationResult( 
                                attribute.ErrorMessage ?? string.Format(CultureInfo.CurrentUICulture, "{0} validation failed.", attribute.GetType().Name), property);
                }
            }
        }

        public ValidationResult ValidateProperty(object component, PropertyInfo propertyInfo)
        {
            IEnumerable<ValidationAttribute> attributes = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>();
            foreach (ValidationAttribute attribute in attributes)
            {
                if (!attribute.IsValid(propertyInfo.GetValue(component, null)))
                    return
                        new ValidationResult(
                            attribute.ErrorMessage ?? string.Format(CultureInfo.CurrentUICulture, "{0} validation failed.", attribute.GetType().Name), propertyInfo);
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
