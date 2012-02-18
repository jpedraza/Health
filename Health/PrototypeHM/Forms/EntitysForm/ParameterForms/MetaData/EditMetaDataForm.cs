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
using PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes;
using PrototypeHM.Forms.EntitysForm.ParameterForms.MetaData;
using PrototypeHM.Parameter;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.MetaData
{
    public partial class EditMetaDataForm : DIForm, ICommonFormsFunctions
    {
        public EditMetaDataForm(IDIKernel diKernel, int id)
            : base(diKernel)
        {
            InitializeComponent();
            _dataObject = new MetadataForParameter() {Id = id};
        }

        private MetadataForParameter _dataObject;

        public bool FlagResult = false;

        private IList<ValueTypeOfMetadata> _allValueTypeOfMetadata;
        private IList<ParameterBaseData> _allParameter;

        private void EditMetaDataFormLoad(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            DetailEditParam();
            this.GetPositiveNotice("Данные подгружены");
        }

        private void DetailEditParam()
        {
            var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<MetadataForParameter>)) as
                                                  OperationsContext<MetadataForParameter>;

            if (oC == null)
                throw new Exception("Отсутсвует контекст операции");

            var @delegate = oC.Detail;
            if (@delegate == null)
                throw new Exception("Отсутсвует операция детализации объекта");

            _dataObject = @delegate(_dataObject) as MetadataForParameter;

            if (_dataObject == null)
            {
                throw new Exception("Пустой объект");
            }
            textBoxKey.Text = _dataObject.Key;
            textBoxValue.Text = Encoding.UTF8.GetString(_dataObject.Value);

            GetParameter();
            GetValueTypes();

            foreach (var parameterBaseData in _allParameter.Where(parameterBaseData => parameterBaseData.ParameterId == _dataObject.ParameterId))
            {
                comboBoxParameters.SelectedIndex = _allParameter.IndexOf(parameterBaseData);
            }

            foreach (var valueTypeOfMetadata in _allValueTypeOfMetadata.Where(valueTypeOfMetadata => valueTypeOfMetadata.ValueTypeId.ToString() == _dataObject.ValueTypeId))
            {
                comboBoxvalueTypes.SelectedIndex = _allValueTypeOfMetadata.IndexOf(valueTypeOfMetadata);
            }
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

        private void Button2Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxKey.Text))
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

                var @delegate = oC.Update;
                if (@delegate == null)
                    throw new Exception("Отсутствует операция обновления данных");

                var o = new MetadataForParameter();
                if (_allParameter.Count > comboBoxParameters.SelectedIndex)
                    o.ParameterId = _allParameter[comboBoxParameters.SelectedIndex].ParameterId;
                o.Key = textBoxKey.Text;
                o.Id = _dataObject.Id;
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
    }
}
