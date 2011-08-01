using System;
using Health.API;
using Health.API.Entities;

namespace Health.Data.Validators
{
    /// <summary>
    /// Фабрика валдаторов
    /// </summary>
    public class ValidatorFactory : IValidatorFactory
    {
        #region IValidatorFactory Members

        /// <summary>
        /// Сообщение почему не прошла валидация
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Валидация значение, заданным типом валидатора
        /// </summary>
        /// <param name="validator_type">Тип валидатора</param>
        /// <param name="value">Значение</param>
        /// <returns>Результат валидации</returns>
        public bool IsValid(string validator_type, object value)
        {
            var validator = (IValueValidator) Activator.CreateInstance(Type.GetType(validator_type));
            if (validator.Equals(null))
            {
                return false;
            }
            bool result = validator.IsValid(value);
            if (!result)
            {
                Message = validator.Message;
            }
            return result;
        }

        #endregion
    }
}