//TODO: Убрать атрибут Период для Параметра
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    public class Parameter : IEntity, IKey
    {
        /// <summary>
        /// Текстовое название параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра (если параметр с вариантами, то сумма баллов).
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Мета-данные (описывают характеристику параметра).
        /// </summary>
        public MetaData MetaData { get; set; }

        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}