﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;
using Health.Site.Areas.Parameters.Models.Forms;
using Health.Core.API.Repository;

namespace Health.Site.Areas.Parameters.Models
{
    public class ParametersViewModel : CoreViewModel
    {
        /// <summary>
        /// Все параметры здоровья
        /// </summary>
        public IList<Parameter> parameters { get; set; }

        /// <summary>
        ///Служебная переменная
        /// </summary>
        public Nullable<int> display_for { get; set; }
        /// <summary>
        /// Новый параметр здоровья
        /// </summary>
        public Parameter NewParam { get; set; }

        /// <summary>
        /// Данный метод перезаписывает атрибуты параметры здоровья человека,
        /// в соответствии с данными, введенными пользователем на 1 этапе ввода 
        /// </summary>
        /// <param name="form">Форма ввода 1 -го этапа</param>
        /// <returns>Возвращается обновленное состояние модели</returns>
        public virtual ParametersViewModel StartEditParameter(StartAddFormModel form)
        {
            if (this.NewParam == null)
            {
                this.NewParam = new Parameter();
            }
            //this.NewParam.Id = form.
            return this;
        }
        
        /// <summary>
        /// Первая стадия добавления параметра
        /// </summary>
        /// /// <param name="Name">Назвние параметра.</param>
        /// <param name="Value">Значение.</param>
        /// <param name="DefaultValue">Значение по умолчанию?</param>
        /// <param name="Is_childs">Будет ли подпараметр?</param>
        /// <param name="Is_var">Есть ли варианты?</param>
        /// <returns>Какую форму подать следуюущей?</returns>
        public virtual ParametersViewModel StartAddParameter(string Name, object Value, object DefaultValue, bool Is_childs, bool Is_var, bool Is_param, IParameterRepository ParamRepo)
        {
            var res = 0;
            var found_parametr = ParamRepo.GetByValue(Name);
            if (found_parametr == null)
            {
                NewParam = new Parameter
                {
                    Name = Name,
                    Value = Value,
                    DefaultValue = DefaultValue,
                };
            }
            var i = 0;
            var j = 0;
            var parameters = ParamRepo.GetAllParam();
            foreach (var item in parameters)
            {
                if (item.Id != null)
                    i = item.Id;
                if (item.MetaData.Id_cat != null)
                    j = (int)item.MetaData.Id_cat;
            }
            NewParam.Id = i + 1;
            NewParam.MetaData = new MetaData { Is_childs = Is_childs, Is_var = Is_var };
            if (Is_param)
            {
                NewParam.MetaData.Age = null;
                NewParam.MetaData.Obligation = true;
                NewParam.MetaData.period = null;
                NewParam.MetaData.Id_cat = null;
                res = 2;
            }
            else
            {
                res = 1;
                NewParam.MetaData.Id_cat = j + 1;
                NewParam.MetaData.Id_parent = null;
                this.parameters = ParamRepo.GetAllParam();
            }

            return this;
        }

        /// <summary>
        /// Вторя стадия добавления параметра
        /// </summary>
        /// /// <param name="last_model_state">Преидущее состояние модели.</param>
        /// <returns>Обновленная модель</returns>
        public virtual ParametersViewModel NextAddParameter(ParametersViewModel last_model_state)
        {
            this.NewParam = last_model_state.NewParam;
            this.NewParam.MetaData.Age = last_model_state.NextAddForm.Age;
            this.NewParam.MetaData.Obligation = last_model_state.NextAddForm.Obligation;
            this.NewParam.MetaData.period = last_model_state.NextAddForm.Period;
            return this;
        }
        /// <summary>
        /// Данный метод оконочательно "досоздает" новый параметр (в виде поля NewParam)
        /// возвращает обновленное состояние модели. Окончательная запись в источник даннных
        /// происходит в контроллере.
        /// </summary>
        /// <param name="last_model_state">Последнее состояние модели</param>
        /// <returns></returns>
        public virtual ParametersViewModel AddParameter(ParametersViewModel last_model_state)
        {
            this.NewParam = last_model_state.NewParam;
            if (last_model_state.VarForm != null && last_model_state.VarForm.variants != null)
            {
                this.NewParam.MetaData.Variants = new Variant[last_model_state.VarForm.variants.Count];
                for (int i = 0; i < this.NewParam.MetaData.Variants.Length; i++)
                {
                    this.NewParam.MetaData.Variants[i] = new Variant(last_model_state.VarForm.variants[i].Value, last_model_state.VarForm.variants[i].Ball);
                }
            }
            return this;
        }
        /// <summary>
        /// Стартовая форма добавления параметров
        /// </summary>
        public StartAddFormModel StartAddForm { get; set; }
        
        /// <summary>
        /// Вторая форма добавления параметров
        /// </summary>
        public NextAddFormModel NextAddForm { get; set; }

        /// <summary>
        /// Форма для добавления вариантов ответа
        /// </summary>
        public VarFormModel VarForm { get; set; }

        /// <summary>
        /// Проверяет, правильно, ли заполнена форма?
        /// </summary>
        /// <param name="form1">Первая форма</param>
        /// <returns></returns>
        public static bool IsCorrectStage1(StartAddFormModel form1)
        {
            return (form1.DefaultValue != null && form1.Is_childs != null && form1.Is_param != null && form1.Is_var != null && form1.Name != null && form1.Value != null);
        }

        /// <summary>
        /// Проверяет корректность заполнения второй формы
        /// </summary>
        /// <param name="form2">вторая форма</param>
        /// <returns></returns>
        public static bool IsCorrectStage2(Parameter NewParam)
        {
            return (NewParam != null && NewParam.DefaultValue!=null && NewParam.Id != null && NewParam.Value != null);
        }
    }
}