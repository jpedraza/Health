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
        public Type ParentType { get; set; }

        public Type ConfType { get; set; }

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