using System;
using System.Collections.Generic;
namespace Health.Core.Entities.POCO
{    
    /// <summary>
    /// Описывает мета-данные параметра (характеристики параметра здоровья)
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MetaData()
        {
            this.Childs = new List<int>();
            this.Parents = new List<int>();
        }
        
        /// <summary>
        /// Показывает, есть ли на данный параметр варианты ответа
        /// </summary>
        public bool Is_var { get; set; }

        /// <summary>
        /// Возраст, при котором необходимо заполнять данный параметр
        /// </summary>
        public object Age
        {
            get
            {
                if(_age != null)
                {
                    var str = _age as string[];
                    if(str != null)
                    {
                        return str[0];
                    }
                    return _age;
                }
                return _age;
            }

            set { _age = value; }
        }

        private object _age;

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
        /// Хранит Id родителей данного класса
        /// </summary>
        public IList<int> Parents { get; set; }

        /// <summary>
        /// Хранит Id детей данного класса
        /// </summary>
        public IList<int> Childs { get; set; }

        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        public object MinValue
        {
            get
            {
                if (_minValue != null)
                {
                    var str = _minValue as string[];
                    if (str != null)
                    {
                        return str[0];
                    }
                    return _minValue;
                }
                return _minValue;
            }

            set { _minValue = value; }
        }
        private object _minValue;

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public object MaxValue
        {
            get
            {
                if (_maxValue != null)
                {
                    var str = _maxValue as string[];
                    if (str != null)
                    {
                        return str[0];
                    }
                    return _maxValue;
                }
                return _maxValue;
            }

            set { _maxValue = value; }
        }

        private object _maxValue;
        /// <summary>
        /// Хранит варианты ответа на вопросы. Если Is_var == false, равно 0
        /// </summary>
        public Variant[] Variants { get; set; }

    }
}