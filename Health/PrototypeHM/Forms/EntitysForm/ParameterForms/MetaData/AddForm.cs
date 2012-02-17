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

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.MetaData
{
    public partial class AddForm : DIForm, ICommonFormsFunctions
    {
        public AddForm(IDIKernel diKernel)
            : base(diKernel)
        {
            InitializeComponent();
        }

        private IList<ValueTypeOfMetadata> _allValueTypeOfMetadata;

        private IList<ParameterBaseData> _allParameter;

        public bool FlagResult = false;
 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(textBoxKey.Text))
                    throw new Exception("Укажите ключ");

                if (string.IsNullOrEmpty(textBoxValue.Text))
                {
                    throw new Exception("Укажите значение");
                }

                if (comboBoxParameters.SelectedIndex == -1)
                {
                    throw new Exception("Выберите параметр");
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
                if (_allParameter.Count > comboBoxParameters.SelectedIndex)
                    o.ParameterId = _allParameter[comboBoxParameters.SelectedIndex].ParameterId;
                o.Key = textBoxKey.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            GetParameter();
            GetValueTypes();
            this.GetPositiveNotice("Данные успешно подгружены.");
        }

        private void GetParameter()
        {
            var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<ParameterBaseData>)) as
                                                  OperationsContext<ParameterBaseData>;

            if (oC == null)
                throw new Exception("Отсутсвует контекст операции");

            var @delegate = oC.Load;
            if (@delegate == null)
                throw new Exception("Отсутсвует операция загрузки данных");

            _allParameter = @delegate();

            foreach (var parameterDetail in _allParameter)
            {
                comboBoxParameters.Items.Add(parameterDetail.Name);
            }
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
