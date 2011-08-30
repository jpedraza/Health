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
        public object Value { get; set; }

        /// <summary>
        /// Значение по-умолчанию нового параметра
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, значение по умолчанию")]
        public object DefaultValue { get; set; }

        /// <summary>
        /// Есть ли подпараметры
        /// </summary>
        public bool Is_childs { get; set; }

        /// <summary>
        /// Есть ли варианты ответа
        /// </summary>
        public bool Is_var { get; set; }
    }
}