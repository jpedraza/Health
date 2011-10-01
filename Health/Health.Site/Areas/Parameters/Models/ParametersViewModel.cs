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
        /// Редактируемый параметр здоровья человека
        /// </summary>
        public Parameter EditParam { get; set; }
        /// <summary>
        /// Первая стадия добавления параметра
        /// </summary>
        /// /// <param name="Name">Назвние параметра.</param>
        /// <param name="Value">Значение.</param>
        /// <param name="DefaultValue">Значение по умолчанию?</param>
        /// <param name="Is_childs">Будет ли подпараметр?</param>
        /// <param name="Is_var">Есть ли варианты?</param>
        /// <returns>Какую форму подать следуюущей?</returns>
        public virtual void StartAddParameter(IParameterRepository ParamRepo)
        {
            var found_parametr = ParamRepo.GetByValue(StartAddForm.Name);
            if (found_parametr == null)
            {
                create_new_parameter(StartAddForm);
            }
            else
            { throw new Exception(String.Format("Параметр с именем {0} , уже существует", StartAddForm.Name)); }
            var j = set_id(ParamRepo);
            NewParam.MetaData = new MetaData { Is_childs = StartAddForm.Is_childs, Is_var = StartAddForm.Is_var };
            if (StartAddForm.Is_param)
            {
                NewParam.MetaData.Age = null;
                NewParam.MetaData.Obligation = true;
                NewParam.MetaData.Id_cat = null;
            }
            else
            {
                NewParam.MetaData.Id_cat = j + 1;
                NewParam.MetaData.Id_parent = null;
                this.parameters = ParamRepo.GetAllParam();
            }
        }

        /// <summary>
        /// Создает новый параметр здоровья человека
        /// </summary>
        /// <param name="start_add_form">1-я форма ввода.</param>
        private void create_new_parameter(StartAddFormModel start_add_form)
        {
            NewParam = new Parameter
            {
                Name = start_add_form.Name,
                Value = start_add_form.Value,
                DefaultValue = start_add_form.DefaultValue,
            };
        }

        /// <summary>
        /// Данный метод ищет для создаваемого параметра никем не занятый параметр
        /// </summary>
        /// <param name="ParamRepo">Репозиторий параметров</param>
        /// <returns>Id нового параметра</returns>
        private int set_id(IParameterRepository ParamRepo)
        {
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
            return j;
        }

        /// <summary>
        /// Вторя стадия добавления параметра
        /// </summary>
        /// /// <param name="last_model_state">Преидущее состояние модели.</param>
        /// <returns>Обновленная модель</returns>
        public virtual void NextAddParameter()
        {
            if (NewParam == null)
            { throw new Exception("Срелняя стадия добавлени параметра. Данные о создаваемом вами параметры утеряны."); }
            this.NewParam.MetaData.Age = NextAddForm.Age;
            this.NewParam.MetaData.Obligation = NextAddForm.Obligation;
        }
        /// <summary>
        /// Данный метод оконочательно "досоздает" новый параметр (в виде поля NewParam)
        /// возвращает обновленное состояние модели. Окончательная запись в источник даннных
        /// происходит в контроллере.
        /// </summary>
        /// <param name="last_model_state">Последнее состояние модели</param>
        /// <returns></returns>
        public virtual void AddVariants()
        {
            this.NewParam = NewParam;
            if (VarForm != null && VarForm.variants != null)
            {
                this.NewParam.MetaData.Variants = new Variant[VarForm.variants.Count];
                for (int i = 0; i < this.NewParam.MetaData.Variants.Length; i++)
                {
                    this.NewParam.MetaData.Variants[i] = new Variant(VarForm.variants[i].Value, VarForm.variants[i].Ball);
                }
            }
            else
                throw new Exception("Данные о создаваемом вами параметры утеряны.");
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
        /// Проверяет корректность заполнения второй формы
        /// </summary>
        /// <param name="form2">вторая форма</param>
        /// <returns></returns>
        public static bool IsCorrectStage2(Parameter NewParam)
        {
            return (NewParam != null && NewParam.DefaultValue!=null && NewParam.Id != null && NewParam.Value != null);
        }

        /// <summary>
        /// Форма для редактирования параметра здоровья
        /// </summary>
        public EditingFormModel EditingForm { get; set; }

        /// <summary>
        /// Данный метод подготавливает состояние модели
        /// к записи в репозиторий измененый параметр здоровья человека
        /// </summary>
        /// <param name="last_form_model">Последнее состояние млдели</param>
        /// <returns>Измененное состояние модели</returns>
        public virtual void SaveEditngParameter()
        {
            EditParam.Name = EditingForm.Name;
            EditParam.Value = EditingForm.Value;
            EditParam.DefaultValue = EditingForm.DefaultValue;
            EditParam.MetaData.Age = EditingForm.Age;
            EditParam.MetaData.Id_cat = EditingForm.Id_cat;
            if (EditingForm.variants != null)
            {
                for (int i = 0; i < EditingForm.variants.Count; i++)
                {
                    EditParam.MetaData.Variants[i] = EditingForm.variants[i];
                }
            }
        }

        /// <summary>
        /// Удаляет вариант ответа из параметра
        /// </summary>
        /// <param name="variant_id">Id удаляемого варианта</param>
        /// <param name="parameter">Параметр, у которого удаляют вариант ответа</param>
        public static void DeleteVariant(int variant_id, Parameter parameter)
        {
            var variants = new List<Variant>(parameter.MetaData.Variants);
            variants.RemoveAt(variant_id);
            parameter.MetaData.Variants = variants.ToArray();
            if (parameter.MetaData.Variants.Length == 0)
            {
                parameter.MetaData.Is_var = false;
                parameter.MetaData.Variants = null;
            }
        }

        /// <summary>
        /// Добавляет новый вариант ответа к текущему параметру.
        /// </summary>
        public void AddVariant()
        {
            var list = new List<Variant>(EditParam.MetaData.Variants);
            list.Add(VarForm.variants.First());
            EditParam.MetaData.Variants = list.ToArray();
        }
    }
}