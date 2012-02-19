using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.Forms.EntitysForm.ParameterForms.Other;
using PrototypeHM.Parameter;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.Parameter
{
    public partial class AddMetadataForParameterForm : DIForm, ICommonFormsFunctions
    {
        public AddMetadataForParameterForm(IDIKernel diKernel, int parameterId)
            : base(diKernel)
        {
            InitializeComponent();
            _parameeterId = parameterId;
        }

        private IList<ValueTypeOfMetadata> _allValueTypeOfMetadata;
        private int _parameeterId;
        public bool FlagResult = false;

        private void AddMetadataForParameterFormLoad(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            GetValueTypes();
            this.GetPositiveNotice("Данные успешно подгружены.");
        }

        private void Button1Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxKey.Text))
                    throw new Exception("Укажите ключ");

                if (string.IsNullOrEmpty(textBoxValue.Text))
                {
                    throw new Exception("Укажите значение");
                }

                
                if (comboBoxvalueTypes.SelectedIndex == -1)
                {
                    throw new Exception("Выберите тип");
                }

                var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(_o => _o.GetType() == typeof(OperationsContext<MetadataForParameter>)) as
                                                  OperationsContext<MetadataForParameter>;

                if (oC == null)
                    throw new Exception("Отсутствует контекст операции");

                var @delegate = oC.Save;
                if (@delegate == null)
                    throw new Exception("Отсутствует операция сохранения данных");

                var o = new MetadataForParameter();
                
                o.Key = textBoxKey.Text;
                o.ParameterId = _parameeterId;
                o.Value = Encoding.UTF8.GetBytes(textBoxValue.Text);
                if (_allValueTypeOfMetadata.Count > comboBoxvalueTypes.SelectedIndex)
                    o.ValueTypeId = _allValueTypeOfMetadata[comboBoxvalueTypes.SelectedIndex].ValueTypeId.ToString();

                var qS = @delegate(o);

                if (qS.Status == 1)
                {
                    FlagResult = true;
                    Close();
                }
                else
                {
                    this.GetNegativeNotice(qS.StatusMessage);
                }
            }
            catch (Exception exp)
            {
                this.GetNegativeNotice(exp.Message);
            }
        }

        private void Button2Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GetValueTypes()
        {
            var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<ValueTypeOfMetadata>)) as
                                                  OperationsContext<ValueTypeOfMetadata>;

            if (oC == null)
                throw new Exception("Отсутсвует контекст операции");

            var @delegate = oC.Load;
            if (@delegate == null)
                throw new Exception("Отсутсвует операция загрузки данных");

            _allValueTypeOfMetadata = @delegate();

            foreach (var valueType in _allValueTypeOfMetadata)
            {
                comboBoxvalueTypes.Items.Add(valueType.Name);
            }
        }
    }
}
