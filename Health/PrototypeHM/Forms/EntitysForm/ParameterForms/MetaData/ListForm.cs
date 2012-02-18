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
            ShowNotice();
        }

        private void ShowNotice()
        {
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
            UpdateTable();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            var form = new AddForm(DIKernel);
            form.ShowDialog();

            this.GetPositiveNotice(form.FlagResult ? "Успешно записано." : "Пользователь отменил действие");
            UpdateTable();
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
                dataGridView1.Rows[index].Cells["Id"].Value = metadataForParameter.Id;

                //Заполнить!!!
                dataGridView1.Rows[index].Cells["ParameterName"].Value = metadataForParameter.ParameterName;
                dataGridView1.Rows[index].Cells["ValueType"].Value = metadataForParameter.ValueTypeName;

                
                index++;
            }
        }
        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            GetTableFromDb();
        }

        private void DataGridView1CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView != null && (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0||e.RowIndex == dataGridView.RowCount -1)) return;

            //Обработка ячеек
            dataGridView1.ClearSelection();
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;


            var cMs = new ContextMenuStrip();
            cMs.Items.Add("Редактировать");
            cMs.Items.Add("Удалить");

            cMs.Items[0].Click += delegate
            {
                var value = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                if (value == (object)string.Empty) return;
                var editForm = new EditMetaDataForm(DIKernel,
                                            Convert.ToInt32(
                                                value));
                editForm.ShowDialog();
                if (editForm.FlagResult)
                {
                    this.GetPositiveNotice("Успешно изменено");
                }
                else
                {
                    this.GetNegativeNotice("Редактирование отменено пользователем");
                }
                UpdateTable();


               

                //TODO: тестинг

                //TODO: попробовать подгружать массивы непосредственно из процедуры
                //
            };

            cMs.Items[1].Click += delegate
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
                        _listData.FirstOrDefault(
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
