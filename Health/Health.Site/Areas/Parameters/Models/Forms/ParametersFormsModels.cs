using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Health.Site.Areas.Parameters.Models.Forms
{
    /// <summary>
    /// Модель формы начала добавления нового параметра
    /// </summary>
    public class StartAddFormModel
    {
        /// <summary>
        /// Название нового параметра
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, название параметра")]
        public string Name { get; set; }

        /// <summary>
        /// Значение нового параметра
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, значение нового параметра")]
        public string Value { get; set; }

        /// <summary>
        /// Значение по-умолчанию нового параметра
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, значение по умолчанию")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Является ли подпараметром?
        /// </summary>
        public bool Is_param { get; set; }

        /// <summary>
        /// Есть ли подпараметры?
        /// </summary>
        public bool Is_childs { get; set; }

        /// <summary>
        /// Есть ли варианты ответа
        /// </summary>
        public bool Is_var { get; set; }
    }

    /// <summary>
    /// Вторая стадия добавления нового параметра
    /// </summary>
    public class NextAddFormModel
    {
        /// <summary>
        /// Возраст
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, возраст")]
        public string Age { get; set; }

        /// <summary>
        /// Обязателен ли к заполнению?
        /// </summary>
        public bool Obligation { get; set; }

        /// <summary>
        /// Период заполнения
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, период")]
        public Nullable<double> Period { get; set; }

        /// <summary>
        /// Число вариантов (если есть)
        /// </summary>
        public Nullable<int> NumVariant { get; set; }

        /// <summary>
        /// Выбрать родителя
        /// </summary>
        public Nullable<int> Parents { get; set; }
    }

    /// <summary>
    /// Форма для добавления вариантов ответа на вопросы
    /// </summary>
    public class VarFormModel
    {
        /// <summary>
        /// Варианты ответов на вопросы
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, вариант")]
        public IList<string> variants { get; set; }

        /// <summary>
        /// Баллы за определенный ответ
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, вариант")]
        public IList<Nullable<double>>balls { get; set; }
    }
}