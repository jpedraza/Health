using System.Collections.Generic;
using System;
namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Описывает мета-данные параметра (характеристики параметра здоровья)
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// Показывает, есть ли на данный параметр варианты ответа
        /// </summary>
        public bool Is_var { get; set; }

        /// <summary>
        /// Возраст, при котором необходимо заполнять данный параметр
        /// </summary>
        public object Age { get; set; }

        /// <summary>
        /// Номер категории 
        /// </summary>
        public Nullable<int> Id_cat { get; set; }

        /// <summary>
        /// Обязательно или нет заполнять данный параметр, подавать или нет его в первичной анкете
        /// </summary>
        public bool Obligation { get; set; }

        /// <summary>
        /// Есть ли подпараметр?
        /// </summary>
        public bool Is_childs { get; set; }

        /// <summary>
        /// Показывает для данного параметра, параметр-родитель. В случае если это есть самостоятельный параметр, равно null.
        /// </summary>
        public Nullable<int> Id_parent { get; set; }
        /// <summary>
        /// Хранит варианты ответа на вопросы. Если Is_var == false, равно 0
        /// </summary>
        public Variant[] Variants { get; set; }

    }
}