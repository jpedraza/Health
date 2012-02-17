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

        private void ListFormLoad(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            GetListAndSetTable();
            this.GetPositiveNotice(dataGridView1.RowCount > 1 ? "Таблица успешно заполнена" : "В базе нет типов.");
        }

        private IList<ValueTypeOfMetadata> _listObject;
        private void GetListAndSetTable()
        {
            try
            {
               Action<ValueTypeOfMetadata, int> iM = (ValueTypeOfMetadata valueTypeOfMetadata, int index) =>
                                {
                                    dataGridView1.RowCount++;
                                    dataGridView1.Rows[index].Cells["ValueTypeId"] = new DataGridViewTextBoxCell()
                                                                                         {
                                                                                             Value =
                                                                                                 valueTypeOfMetadata.
                                                                                                 ValueTypeId
                                                                                         };
                                                                                            

                                        dataGridView1.Rows[index].Cells["NameValueType2"] = new DataGridViewTextBoxCell()
                                                                                                {
                                                                                                    Value = valueTypeOfMetadata.Name
                                                                                                };
                                };
                this.MakeLoadAllData<ValueTypeOfMetadata>(iM, ref _listObject, this);
            }
            catch (Exception exp)
            {
                YMessageBox.Error(string.Format("Пользовательское исключение: {0}", exp.Message));
                Close();
            }
        }

        private void DataGridView1CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            
            //Обработка ячеек
            dataGridView1.ClearSelection();
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;


            var cMs = new ContextMenuStrip();
            cMs.Items.Add("Редактировать");
            cMs.Items.Add("Удалить");

            cMs.Items[0].Click += delegate
                                      {
                                          var value = dataGridView1.Rows[e.RowIndex].Cells["ValueTypeId"].Value;
                                          if (value == (object) string.Empty) return;
                                          var editForm = new EditForm(DIKernel,
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
                                      };

            cMs.Items[1].Click += delegate
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
                                                  _listObject.FirstOrDefault(
                                                      o =>
                                                      o.ValueTypeId ==
                                                      Convert.ToInt32(
                                                          dataGridView1.Rows[e.RowIndex].Cells["ValueTypeId"].Value));

                                              var qS = @delegate(dO);
                                              if (qS.Status ==1)
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
                                          catch(Exception exp)
                                          {
                                              YMessageBox.Error(exp.Message);
                                          }
                                      };
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ContextMenuStrip = cMs;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
