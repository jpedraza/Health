using System;
using System.Linq.Expressions;

namespace Health.Site.Extensions
{
    /// <summary>
    /// Класс методов-расширений для объектов типа object.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Получить имя свойства.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <typeparam name="TOut">Тип свойства.</typeparam>
        /// <param name="source">Расширяемый объект.</param>
        /// <param name="property">Свойство объекта.</param>
        /// <returns>Имя свойства.</returns>
        public static string PropertyName<T, TOut>(this T source, Expression<Func<T, TOut>> property)
        {
            var member_expression = (MemberExpression) property.Body;
            return member_expression.Member.Name;
        }
    }
}