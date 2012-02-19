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
using PrototypeHM.Forms.EntitysForm.ParameterForms.MetaData;
using PrototypeHM.Forms.EntitysForm.ParameterForms.Other;
using PrototypeHM.Parameter;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.Parameter
{
    public partial class ListParametersForm : DIForm, ICommonFormsFunctions
    {
        private IList<ParameterBaseData> _listData;

        public ListParametersForm(IDIKernel diKernel)
            : base(diKernel)
        {
            InitializeComponent();
        }

        private void ListParametersForm_Load(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            GetTableFromDb();
            ShowNotice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AddParameterForm(DIKernel);
            form.ShowDialog();

            this.GetPositiveNotice(form.FlagResult ? "Успешно записано." : "Пользователь отменил действие");
            UpdateTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GetTableFromDb()
        {
            var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<ParameterBaseData>)) as
                                                  OperationsContext<ParameterBaseData>;

            if (oC == null)
                throw new Exception("Отсутсвует контекст операции");

            var @delegate = oC.Load;
            if (@delegate == null)
                throw new Exception("Отсутсвует операция загрузки данных");

            _listData = @delegate();
            var index = 0;
            foreach (var parameterBaseData in _listData)
            {

                dataGridView1.RowCount++;
                dataGridView1.Rows[index].Cells["Id"].Value = parameterBaseData.ParameterId;
                dataGridView1.Rows[index].Cells["NameParameter"].Value = parameterBaseData.Name;
                dataGridView1.Rows[index].Cells["DefaultValue"].Value =
                    Encoding.UTF8.GetString(parameterBaseData.DefaultValue);
                index++;
            }
        }

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            GetTableFromDb();
        }
        private void ShowNotice()
        {
            if (_listData != null)
            {
                this.GetPositiveNotice(_listData.Count > 0 ? "Данные загружены" : "В Базе нет записей");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView != null && (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex == dataGridView.RowCount - 1)) return;

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
                var editForm = new EditParameterForm(DIKernel,
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

                //TODO: попробовать подгружать массивы непосредственно из процедуры
                //
            };

            cMs.Items[1].Click += delegate
            {
                try
                {
                    var oC =
                        DIKernel.Get<OperationsRepository>().Operations.FirstOrDefault(
                            o => o.GetType() == typeof(OperationsContext<ParameterBaseData>)) as
                        OperationsContext<ParameterBaseData>;

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

        //TODO: для листинга скопировать с листинга метаданных. Заменить тип метаданных на тип параметра
        //TODO:     убрать поля связанные с метадаными

        //TODO: для добавления скопировать. удалить списки параметров, типов метаданнных. добавить список метаданных
        //TODO: замена элементов управления
        //TODO: скопировать метод сбора данных. имзенить его. учесть bytes[]

        //TODO: для редактирования скопировать и заменить на обновление 

        //TODO: скопировать и заменить методу удаления
    }
}
