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
            NewParam.Id = i;
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
                NewParam.MetaData.Id_cat = j;
                this.parameters = ParamRepo.GetAllParam();
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

        public VarFormModel VarForm { get; set; }

    }
}