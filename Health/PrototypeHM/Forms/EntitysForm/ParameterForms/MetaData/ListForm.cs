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
    public partial class ListForm : DIForm, ICommonFormsFunctions
    {
        public ListForm(IDIKernel diKernel)
            : base(diKernel)
        {
            InitializeComponent();
            _listData = new List<MetadataForParameter>();
        }

        private IList<MetadataForParameter> _listData;

        private void ListFormLoad(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            GetTableFromDb();
            if (_listData != null)
            {
                this.GetPositiveNotice(_listData.Count > 0 ? "Данные загружены" : "В Базе нет записей");
            }
        }

        private void Button3Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2Click(object sender, EventArgs e)
        {

        }

        private void Button1Click(object sender, EventArgs e)
        {
            var form = new AddForm(DIKernel);
            form.ShowDialog();

            this.GetPositiveNotice(form.FlagResult ? "Успешно записано." : "Пользователь отменил действие");
        }

        private void GetTableFromDb()
        {
            var oC =DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<MetadataForParameter>)) as
                                                  OperationsContext<MetadataForParameter>;

            if (oC == null)
                throw new Exception("Отсутсвует контекст операции");

            var @delegate = oC.Load;
            if (@delegate == null)
                throw new Exception("Отсутсвует операция загрузки данных");

            _listData = @delegate();
            var index = 0;
            foreach (var metadataForParameter in _listData)
            {

                dataGridView1.RowCount++;
                dataGridView1.Rows[index].Cells["ParameetrerId"].Value = metadataForParameter.ParameterId;
                dataGridView1.Rows[index].Cells["Key"].Value = metadataForParameter.Key;
                dataGridView1.Rows[index].Cells["Value"].Value = Encoding.UTF8.GetString(metadataForParameter.Value);
                dataGridView1.Rows[index].Cells["ValueTypeId"].Value = metadataForParameter.ValueTypeId;

                //Заполнить!!!
                dataGridView1.Rows[index].Cells["ParameterName"].Value = metadataForParameter.ParameterName;
                dataGridView1.Rows[index].Cells["ValueType"].Value = metadataForParameter.ValueTypeName;

                
                index++;
            }
        }
    }
}
