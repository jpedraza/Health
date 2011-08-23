namespace Health.Core.API.Validators
{
    /// <summary>
    /// Интерфейс валидатора.
    /// </summary>
    public interface IValueValidator
    {
        /// <summary>
        /// Сообщение с информацией почему не прошла проверка.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Метод проверяет значение на валидность.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Результат проверки.</returns>
        bool IsValid(object value);
    }
}