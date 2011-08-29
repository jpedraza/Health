using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Health.Site.Models.Rules;

namespace Health.Site.Models.Configuration
{
    /// <summary>
    /// Метаданные для конкретного свойства модели.
    /// </summary>
    public class ModelMetadataPropertyConfiguration
    {
        /// <summary>
        /// Родительский тип модели.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Если от формы получена пустая строка она преобразуется в null.
        /// </summary>
        public bool ConvertEmptyStringToNull { get; set; }

        /// <summary>
        /// Описание объекта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Формат выводо объекта.
        /// </summary>
        public string DisplayFormatString { get; set; }

        /// <summary>
        /// ФОрмат вывода объета в формах редактирования.
        /// </summary>
        public string EditFormatString { get; set; }

        /// <summary>
        /// Вывод значения объекта без дополнительных html тегов.
        /// </summary>
        public bool HideSurroundingHtml { get; set; }

        /// <summary>
        /// Значение объекта только для чтения.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Текст отображаемый если значение объекта равно null.
        /// </summary>
        public string NullDisplayText { get; set; }

        /// <summary>
        /// Краткое имя объекта.
        /// </summary>
        public string ShortDisplayName { get; set; }

        /// <summary>
        /// Необходимо ли отображать объект.
        /// </summary>
        public bool ShowForDisplay { get; set; }

        /// <summary>
        /// Необходимо ли отображать объект в формах редактирования.
        /// </summary>
        public bool ShowForEdit { get; set; }

        /// <summary>
        /// Текст который выводится вместо комплексного значения объекта.
        /// </summary>
        public string SimpleDisplayText { get; set; }

        /// <summary>
        /// Шаблон для вывода значения.
        /// </summary>
        public string TemplateHint { get; set; }

        /// <summary>
        /// Водяной знак.
        /// </summary>
        public string Watermark { get; set; }

        /// <summary>
        /// Отображаемое имя.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Является ли поле обязательным для заполнения.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Тип данных свойства.
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// Дополнительные атрибуты.
        /// </summary>
        public Dictionary<string, object> AdditionalValues { get; set; }

        /// <summary>
        /// Правила валидации для свойства.
        /// </summary>
        public IList<IValidatorRuleConfig> ValidatorRule { get; set; }
    }
}

        