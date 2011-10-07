using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Site.Models;
using Health.Site.Areas.Admin.Models.Forms;
using Health.Core.API.Repository;

namespace Health.Site.Areas.Admin.Models
{
    public class ParametersViewModel : CoreViewModel
    {
        /// <summary>
        /// Форма добавления нового параметра.
        /// </summary>
        public AddFormModel AddForm { get; set; }

        /// <summary>
        /// Форма для добавления вариантов ответа
        /// </summary>
        public VarFormModel VarForm { get; set; }

        /// <summary>
        /// Форма для редактирования параметра здоровья
        /// </summary>
        public EditingFormModel EditingForm { get; set; }

        /// <summary>
        /// Все параметры здоровья
        /// </summary>
        public IList<Parameter> parameters { get; set; }

        int id_cat { get; set; }

        /// <summary>
        /// Новый параметр здоровья
        /// </summary>
        public Parameter NewParam { get; set; }

        #region AddWithOutVariants

        public void SetPropertiesAndMetadata()
        {
            set_id();
            SetParentsAndChildren();
            SetOther();
        }

        void SetParentsAndChildren()
        {
            for (var i = 0; i < AddForm.Parameters.Count; i++)
            {
                if (AddForm.CheckBoxesParents[i])
                {
                    AddForm.Parameters[i].MetaData.Childs.Add(AddForm.parameter.Id);
                    AddForm.parameter.MetaData.Parents.Add(AddForm.Parameters[i].Id);
                }

                if (AddForm.CheckBoxesChildren[i])
                {
                    AddForm.parameter.MetaData.Childs.Add(AddForm.Parameters[i].Id);
                    AddForm.Parameters[i].MetaData.Parents.Add(AddForm.parameter.Id);
                }
            }
        }

        public void SaveParentsAndChildren()
        {
            for (var i = 0; i < EditingForm.Parameters.Count; i++)
            {
                if (EditingForm.CheckBoxesParents[i])
                {
                    EditingForm.Parameters[i].MetaData.Childs.Add(EditingForm.parameter.Id);
                    EditingForm.parameter.MetaData.Parents.Add(EditingForm.Parameters[i].Id);
                }

                if (EditingForm.CheckBoxesChildren[i])
                {
                    EditingForm.parameter.MetaData.Childs.Add(EditingForm.Parameters[i].Id);
                    EditingForm.Parameters[i].MetaData.Parents.Add(EditingForm.parameter.Id);
                }
            }
        }

        void SetOther()
        {
            AddForm.parameter.MetaData.Id_cat = id_cat;
        }

        private void set_id()
        {
            var i = 0;
            var j = 0;
            for (int ind = 0; ind < AddForm.Parameters.Count; ind++)
            {
                i = ind;
                if (AddForm.Parameters[ind].MetaData.Id_cat != null)
                    j = (int)AddForm.Parameters[ind].MetaData.Id_cat;
            }
            AddForm.parameter.Id = i + 1;
            id_cat = j + 1;
        }
        #endregion
        /// <summary>
        /// Редактируемый параметр здоровья человека
        /// </summary>
    }
}