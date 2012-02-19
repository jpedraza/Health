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
    public partial class EditParameterForm : DIForm, ICommonFormsFunctions
    {
        public EditParameterForm(IDIKernel diKernel, int id)
            : base(diKernel)
        {
            InitializeComponent();
            _dataObject = new ParameterDetail() { ParameterId = id };
        }


        private ParameterDetail _dataObject;

        private IList<MetadataForParameter> _metadatas; 

        public bool FlagResult = false;

        private void EditParameterFormLoad(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            SetTable();
            this.GetPositiveNotice();
        }

        private void DetailParameter ()
        {
            var oC = DIKernel.Get<OperationsRepository>().
               Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<ParameterDetail>)) as
                                                 OperationsContext<ParameterDetail>;

            if (oC == null)
                throw new Exception("Отсутсвует контекст операции");

            var @delegate = oC.Detail;
            if (@delegate == null)
                throw new Exception("Отсутсвует операция детализации объекта");

            _dataObject = @delegate(_dataObject) as ParameterDetail;

            if (_dataObject == null)
            {
                throw new Exception("Пустой объект");
            }
            textBoxDefaultValue.Text = Encoding.UTF8.GetString(_dataObject.DefaultValue);
            textBoxNameParameter.Text = _dataObject.Name;
            _metadatas = _dataObject.Metadata;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxNameParameter.Text))
                    throw new Exception("Укажите имя");

                if (string.IsNullOrEmpty(textBoxDefaultValue.Text))
                {
                    throw new Exception("Укажите значение по умолчанию");
                }

                var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(_o => _o.GetType() == typeof(OperationsContext<ParameterDetail>)) as
                                                  OperationsContext<ParameterDetail>;

                if (oC == null)
                    throw new Exception("Отсутствует контекст операции");

                var @delegate = oC.Update;
                if (@delegate == null)
                    throw new Exception("Отсутствует операция сохранения данных");

                var o = new ParameterDetail()
                            {
                                ParameterId = _dataObject.ParameterId,
                                Name = textBoxNameParameter.Text,
                                DefaultValue = Encoding.UTF8.GetBytes(textBoxDefaultValue.Text)
                            };


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

        private void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new AddMetadataForParameterForm(DIKernel, _dataObject.ParameterId);
            form.ShowDialog();

            this.GetPositiveNotice(form.FlagResult ? "Успешно записано." : "Пользователь отменил действие");
            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            SetTable();
        }

        private void SetTable()
        {
            DetailParameter();
            var index = 0;
            foreach (var metadataForParameter in _metadatas)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[index].Cells["Id"].Value = metadataForParameter.Id;
                dataGridView1.Rows[index].Cells["KeyMetadata"].Value = metadataForParameter.Key;
                dataGridView1.Rows[index].Cells["ValueMetadata"].Value = Encoding.UTF8.GetString(metadataForParameter.Value);
                dataGridView1.Rows[index].Cells["ValueTypeMetadata"].Value = metadataForParameter.ValueTypeName;
                index++;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateTable();
        }

        private void DataGridView1MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void DataGridView1CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView != null && (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex == dataGridView.RowCount - 1)) return;
            
            dataGridView1.ClearSelection();
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

            var cMs = new ContextMenuStrip();
            cMs.Items.Add("Удалить");

            cMs.Items[0].Click += delegate
            {
                try
                {
                    var oC =
                        DIKernel.Get<OperationsRepository>().Operations.FirstOrDefault(
                            o => o.GetType() == typeof(OperationsContext<MetadataForParameter>)) as
                        OperationsContext<MetadataForParameter>;

                    if (oC == null)
                        throw new Exception("Отсутствует контекст операции");

                    var @delegate = oC.Delete;
                    if (@delegate == null)
                        throw new Exception("Отсутствует операция удаления");

                    var dO =
                        _metadatas.FirstOrDefault(
                            o =>
                            o.Id ==
                            Convert.ToInt32(
                                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value)));

                    var qS = @delegate(dO);
                    if (qS.Status == 1)
                    {
                        //YMessageBox.Information("Успешно удалено");
                        this.UpdateTable();
                        this.GetPositiveNotice(
                                               dataGridView1.RowCount > 1
                                                   ? "Успешно удалено"
                                                   : "Успешно удалено. В базе нет типов.");
                    }
                    else
                    {
                        YMessageBox.Error(qS.StatusMessage);
                    }
                }
                catch (Exception exp)
                {
                    YMessageBox.Error(exp.Message);
                }
            };
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = cMs;
        }
    }
}
