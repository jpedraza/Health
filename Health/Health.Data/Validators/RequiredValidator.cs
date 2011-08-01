using System;
using Health.API.Entities;

namespace Health.Data.Validators
{
    /// <summary>
    /// Валидатор требует, чтобы значение не было пустой строкой.
    /// </summary>
    public class RequiredValidator : IValueValidator
    {
        #region IValueValidator Members

        /// <summary>
        /// Сообщени почему не прошла валидация.
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Валидация значения.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Результат валидации.</returns>
        public bool IsValid(object value)
        {
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            Message = "Значение не может быть пустым";
            return false;
        }

        #endregion
    }
}