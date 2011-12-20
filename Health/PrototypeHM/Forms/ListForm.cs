using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;

namespace PrototypeHM.Forms
{
    public partial class ListForm<TData> : DIForm
        where TData : class
    {
        private IList<TData> _data;

        public Func<IList<TData>> LoadData { get; set; }

        public Func<TData, QueryStatus> DeleteData { get; set; }

        public Func<TData, object> DetailData { get; set; }

        private object DefaulDetailData(TData data)
        {
            return data;
        }

        public ListForm()
        {
            BaseConstructor();
        }

        public ListForm(IDIKernel diKernel) : base(diKernel)
        {
            BaseConstructor();
        }

        private void BaseConstructor()
        {
            DetailData = DefaulDetailData;
            InitializeComponent();
        }

        public void InitializeOperations()
        {
            var operationsContext =
                DIKernel.Get<OperationsRepository>().Operations.Where(
                    o => o.GetType() == typeof (OperationsContext<TData>)).FirstOrDefault() as
                OperationsContext<TData>;
            if (operationsContext != null)
            {
                LoadData = operationsContext.Load;
                DeleteData = operationsContext.Delete;
                DetailData = operationsContext.Detail ?? DefaulDetailData;
            }
            else
            {
                DetailData = DefaulDetailData;
            }
            if (LoadData != null)
            {
                var el = new ToolStripButton
                             {
                                 DisplayStyle = ToolStripItemDisplayStyle.Text,
                                 Text = @"Обновить"
                             };
                el.Click += (sender, e) => RefreshData();
                ydgvwc.tsOperations.Items.Add(el);
            }
            if (DeleteData != null)
            {
                var el = new ToolStripButton
                             {
                                 DisplayStyle = ToolStripItemDisplayStyle.Text,
                                 Text = @"Удалить"
                             };
                Action<int> deleteAction = clickedRow =>
                                               {
                                                   QueryStatus status = DeleteData(_data[clickedRow]);
                                                   if (status.Status == 0)
                                                       YMessageBox.Information(status.StatusMessage);
                                               };
                el.Click += (sender, e) =>
                                {
                                    if (ydgvwc.ydgvData.SelectedCells.Count == 0)
                                    {
                                        YMessageBox.Information(@"Выберите строку для удаления");
                                    }
                                    else
                                    {
                                        int selectedIndex = ydgvwc.ydgvData.SelectedCells[0].RowIndex;
                                        deleteAction(selectedIndex);
                                    }
                                };
                ydgvwc.ydgvData.Delete = deleteAction;
                ydgvwc.tsOperations.Items.Add(el);
            }
            if (DetailData != null)
            {
                var el = new ToolStripButton
                             {
                                 DisplayStyle = ToolStripItemDisplayStyle.Text,
                                 Text = @"Подробнее"
                             };
                Action<int> detail = clickedRow =>
                                         {
                                             var form = new DetailForm(DIKernel, DetailData(_data[clickedRow]))
                                                            {MdiParent = MdiParent};
                                             form.Show();
                                         };
                el.Click += (sender, e) =>
                                {
                                    if (ydgvwc.ydgvData.SelectedCells.Count == 0)
                                    {
                                        YMessageBox.Information(@"Выберите строку для просмотра подробных данных.");
                                    }
                                    else
                                    {
                                        int selectedIndex = ydgvwc.ydgvData.SelectedCells[0].RowIndex;
                                        detail(selectedIndex);
                                    }
                                };
                ydgvwc.tsOperations.Items.Add(el);
                ydgvwc.ydgvData.Detail = detail;
            }
            ydgvwc.ydgvData.InitializeOperations();
        }

        private void RefreshData()
        {
            _data = LoadData();
            ydgvwc.ydgvData.DataSource = _data;
        }

        private void ListFormLoad(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
