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
    public partial class ListForm : DIForm, ICommonFormsFunctions
    {
        public ListForm(IDIKernel diKernel)
            : base(diKernel)
        {
            InitializeComponent();
            
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel(this);
            GetListAndSetTable();
            this.GetPositiveNotice(this, dataGridView1.RowCount > 1 ? "Таблица успешно заполнена" : "В базе нет типов.");
        }

        private IList<ValueTypeOfMetadata> ListObject { get; set; } 
        private void GetListAndSetTable()
        {
            try
            {
                //Вначале загружаем данные

                var operationContext =
                    Get<OperationsRepository>().Operations.FirstOrDefault(
                        o => o.GetType() == typeof (OperationsContext<ValueTypeOfMetadata>)) as
                    OperationsContext<ValueTypeOfMetadata>;

                if (operationContext == null) throw new Exception("Отсутствует контекст операций");

                var @delegate = operationContext.Load;
                if (@delegate == null)
                    throw new Exception("Отсутствует операция загрузки");

                ListObject = @delegate();
                if (ListObject.Count <= 0) return;
                var index = -1;
                foreach (var valueTypeOfMetadata in ListObject)
                {
                    dataGridView1.RowCount++;
                    index++;

                    dataGridView1.Rows[index].Cells["ValueTypeId"] = new DataGridViewTextBoxCell()
                                                                         {Value = valueTypeOfMetadata.ValueTypeId};

                    dataGridView1.Rows[index].Cells["NameValueType2"] = new DataGridViewTextBoxCell()
                                                                            {
                                                                                Value = valueTypeOfMetadata.Name
                                                                            };
                }
            }
            catch (Exception exp)
            {
                YMessageBox.Error(string.Format("Пользовательское исключение: {0}", exp.Message));
                Close();
            }
        }

        private void AddValueTypes_Click(object sender, EventArgs e)
        {
            var form = new AddForm(this.DIKernel);
            form.ShowDialog();
            if (form.FlagResult)
            {
                this.GetPositiveNotice(this, "Успешно записано");
            }
            else
            {
                this.GetNegativeNotice(this, "Добавление отменено пользователем");
            }
            Update();

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Update();
            this.GetPositiveNotice(this, "Обновлено");
            this.GetPositiveNotice(this, dataGridView1.RowCount > 1 ? "Таблица успешно заполнена" : "В базе нет типов.");
        }

        private void Update()
        {
            dataGridView1.Rows.Clear();
            GetListAndSetTable();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //Обработка выделений ячеек
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                var cMS = new ContextMenuStrip();

                cMS.Items.Add("Редактировать");
                cMS.Items.Add("Удалить");

                cMS.Items[0].Click += delegate
                                          {
                                              var value = dataGridView1.Rows[e.RowIndex].Cells["ValueTypeId"].Value;
                                              if (value == (object) string.Empty) return;
                                              var editForm = new EditForm(DIKernel,
                                                                          Convert.ToInt32(
                                                                              value));
                                              editForm.ShowDialog();
                                              if (editForm.FlagResult)
                                              {
                                                  this.GetPositiveNotice(this, "Успешно изменено");
                                              }
                                              else
                                              {
                                                  this.GetNegativeNotice(this, "Редактирование отменено пользователем");
                                              }
                                              Update();
                                          };

                cMS.Items[1].Click += delegate
                                          {
                                              try
                                              {
                                                  var oC =
                                                      DIKernel.Get<OperationsRepository>().Operations.FirstOrDefault(
                                                          o => o.GetType() == typeof(OperationsContext<ValueTypeOfMetadata>)) as
                                                      OperationsContext<ValueTypeOfMetadata>;

                                                  if(oC == null)
                                                      throw new Exception("Отсутсвует контекст операции");

                                                  var @delegate = oC.Delete;
                                                  if(@delegate == null)
                                                      throw new Exception("Отсутсвует операция удаления");

                                                  var dO =
                                                      ListObject.FirstOrDefault(
                                                          o =>
                                                          o.ValueTypeId ==
                                                          Convert.ToInt32(
                                                              dataGridView1.Rows[e.RowIndex].Cells["ValueTypeId"].Value));

                                                  var qS = @delegate(dO);
                                                  if (qS.Status ==1)
                                                  {
                                                      //YMessageBox.Information("Успешно удалено");
                                                      this.Update();
                                                      this.GetPositiveNotice(this,
                                                                             dataGridView1.RowCount > 1
                                                                                 ? "Успешно удалено"
                                                                                 : "Успешно удалено. В базе нет типов.");
                                                  }
                                                  else
                                                  {
                                                      YMessageBox.Error(qS.StatusMessage);
                                                  }
                                              }
                                              catch(Exception exp)
                                              {
                                                  YMessageBox.Error(exp.Message);
                                              }
                                          };
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = cMS;
                
            }
        }
    }
}
