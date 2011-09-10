using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using Health.Core.Entities.POCO;

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
        [CannotBeEmptyAtribute]
        public IList<Variant> variants { get; set; }
    }

    /// <summary>
    /// Данный класс описывает валидатор для проверки:
    /// Все ли элементы списка на форме заполнены?
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CannotBeEmptyAtribute : ValidationAttribute
    {
        private const string defaultError = "'{0}' must have at least one element";
        public CannotBeEmptyAtribute()
            : base(defaultError)
        {
        }

        public override bool IsValid(object value)
        {
            IList<Variant> list = value as IList<Variant>;
            if (list == null || list.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (Variant variant in list)
                {
                    if (String.IsNullOrEmpty(variant.Value) || String.IsNullOrEmpty(variant.Ball.ToString()))
                        return false;
                    else
                        return true;
                }
            }
            return flag;
        }
        /// <summary>
        /// Служебная переменная, для реализации алгоритма.
        /// </summary>
        private static bool flag = true;
        public override string FormatErrorMessage(string name)
        {
            return String.Format(this.ErrorMessageString, name);
        }
    }

    /// <summary>
    /// Модель формы редактирования уже существующего параметра.
    /// </summary>
    public class EditingFormModel
    {
        /// <summary>
        /// Название параметра здоровья
        /// </summary>
        [Required(ErrorMessage = "Введите название параметра")]
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра здоровья
        /// </summary>
        [Required(ErrorMessage = "Введите значение параметра")]
        public string Value { get; set; }

        /// <summary>
        /// Значение по-умолчанию
        /// </summary>
        [Required(ErrorMessage = "Введите значение по-умолчанию")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        [Required(ErrorMessage = "Введите возраст")]
        public string Age { get; set; }

        /// <summary>
        /// Id_cat
        /// </summary>
        public Nullable<int> Id_cat { get; set; }

        /// <summary>
        /// Варианты ответов на параметр.
        /// </summary>
        [CannotBeEmptyAtribute]
        public IList<Variant> variants { get; set; }
    }
}