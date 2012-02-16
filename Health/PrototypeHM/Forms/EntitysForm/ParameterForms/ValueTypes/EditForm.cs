using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrototypeHM.DB.DI;
using PrototypeHM.Components;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using PrototypeHM.DB.DI;
using PrototypeHM.Properties;
using PrototypeHM;
using PrototypeHM.Parameter;
using PrototypeHM.Forms.EntitysForm.ParameterForms.Other;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes
{
    public partial class EditForm : DIForm, ICommonFormsFunctions
    {
        public EditForm(IDIKernel diKernel, int valuetypeId)
            : base(diKernel)
        {
            InitializeComponent();
            DataObject = new ValueTypeOfMetadata(){ValueTypeId = valuetypeId};
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private ValueTypeOfMetadata DataObject { get; set; }

        public bool FlagResult = false;

        private void SaveClickButtonClick(object sender, EventArgs e)
        {
            try
            {
                //Проверяем на пустое значение
                if (!string.IsNullOrEmpty(NameTextBox.Text))
                {
                    //Получаем делегат сохранения
                    var operationContext =
                    Get<OperationsRepository>().Operations.FirstOrDefault(
                        o => o.GetType() == typeof(OperationsContext<ValueTypeOfMetadata>)) as
                    OperationsContext<ValueTypeOfMetadata>;

                    if (operationContext == null) throw new Exception("Отсутствует контекст операций");

                    var @delegate = operationContext.Update;
                    if (@delegate == null)
                        throw new Exception("Отсутствует метод сохранения.");

                    DataObject.Name = NameTextBox.Text;

                    var qs = @delegate(DataObject);
                    if (qs.Status == 1)
                    {
                        FlagResult = true;
                        Close();
                    }
                    else
                    {
                        YMessageBox.Warning(qs.StatusMessage);
                    }

                }
                else
                {
                    this.GetNegativeNotice(this, "Укажите название!");
                }
            }
            catch (Exception exp)
            {

                YMessageBox.Error(string.Format("Пользовательское исключение: {0}", exp.Message));
                Close();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Проверяем индекс
                if(DataObject.ValueTypeId <=0)
                    throw new Exception("Неправильный Id");

                //Получаем контекст операций
                var oC =
                    Get<OperationsRepository>().Operations.FirstOrDefault(
                        o => o.GetType() == typeof (OperationsContext<ValueTypeOfMetadata>)) as
                    OperationsContext<ValueTypeOfMetadata>;

                if(oC == null)
                    throw new Exception("Отсутствует контекст операций");

                //Получаем делегат получения объекта из БД
                var @delegate = oC.Detail;

                if(@delegate == null)
                    throw new Exception("Отсутствует операция получения объекта");

                //Получаем объект
                DataObject = @delegate(DataObject) as ValueTypeOfMetadata;

                if(DataObject == null)
                    throw new Exception("Отсутствует объект c заданным Id");
                else
                {
                    if (DataObject.Name != null)
                    {
                        NameTextBox.Text = DataObject.Name;

                        this.SwitchOnNoticePanel(this);
                        this.GetPositiveNotice(this, "Успешно загружено");
                    }
                    else throw new Exception("Ошибка репозиторяи - поле со значением null");
                }
            }
            catch (Exception exp)
            {
                YMessageBox.Error(exp.Message);
                Close();
            }
        }
    }
}
