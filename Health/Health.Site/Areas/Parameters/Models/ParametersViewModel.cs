using System;
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
                NewParam = new Parameter { 
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
            this.NewParam.MetaData.Variants = new Variant[last_model_state.VarForm.variants.Count];
            for (int i = 0; i < this.NewParam.MetaData.Variants.Length; i++)
            {
                this.NewParam.MetaData.Variants[i] = new Variant(last_model_state.VarForm.variants[i], last_model_state.VarForm.balls[i]);
                this.NewParam.MetaData.Variants[i].Value = last_model_state.VarForm.variants[i];
                this.NewParam.MetaData.Variants[i].Ball = last_model_state.VarForm.balls[i];
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

    }
}