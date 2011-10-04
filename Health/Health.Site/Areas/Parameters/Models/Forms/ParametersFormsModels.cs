using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using Health.Site.Models.Metadata;
using Health.Site.Models;
using System.ComponentModel;

namespace Health.Site.Areas.Parameters.Models.Forms
{
    /// <summary>
    /// Модель формы начала добавления нового параметра
    /// </summary>
    public class AddFormModel : CoreViewModel
    {
        /// <summary>
        /// Добавляемый параметр.
        /// </summary>
        [ClassMetadata(typeof(ParameterFormMetadata))]
        public Parameter parameter { get; set; }

        /// <summary>
        /// Список всех доступных параметров
        /// </summary>
        public IList<Parameter> Parameters { get; set; }

        /// <summary>
        /// Список флажков для отметки параметров-родителей.
        /// </summary>
        public IList<bool> CheckBoxesParents { get; set; }

        /// <summary>
        /// Список флажков для отметки подпараметров.
        /// </summary>
        public IList<bool> CheckBoxesChildren { get; set; }

        /// <summary>
        /// Служебная переменная - число вариантов ответа
        /// </summary>
        [DisplayName("Число вариантов ответа на вопрос, если есть"),
        Required(ErrorMessage="Укажите корректное значение числа вариантов")]
        public int NumValue { get; set; }
    }
    
    /// <summary>
    /// Форма для добавления вариантов ответа на вопросы
    /// </summary>

    public class VarFormModel : CoreViewModel
    {
        /// <summary>
        /// Варианты ответа на параметр
        /// </summary>
        public Variant[] Variants { get; set; }

        /// <summary>
        /// Служебная переменная, хранит  число вариантов ответа на вопрос
        /// </summary>
        public int NumVariant { get; set; }

        /// <summary>
        /// Хранит создаваемый параметр
        /// </summary>
        [ClassMetadata(typeof(VariantFormMetaData))]
        public Parameter Parameter { get; set; }
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

        
        public Variant[] variants { get; set; }
    }
}