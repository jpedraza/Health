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
        /// Текстовое содержание варианта
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Балл, соответствующий ответу на вопрос
        /// </summary>
        public Nullable<double> Ball { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="value">Значение варианта</param>
        /// <param name="ball">Значение балла за ответ</param>
        public Variant(string value, Nullable<double> ball)
        {
            this.Value = value;
            this.Ball = ball;
        }

        /// <summary>
        /// Конструктор класса без параметров
        /// </summary>
        public Variant()
        { ;}
    }
}
