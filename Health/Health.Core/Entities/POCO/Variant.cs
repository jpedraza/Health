using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Описывает вариант ответа на параметр, если последний является вопросом
    /// </summary>
    public class Variant
    {
        /// <summary>
        /// Текстовое содержание параметра
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Балл, соответствующий ответу на вопрос
        /// </summary>
        public double Ball { get; set; }
    }
}
