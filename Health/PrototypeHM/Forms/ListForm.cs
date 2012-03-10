using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFCFModel;
using PrototypeHM.DI;

namespace PrototypeHM.Forms
{
    public partial class ListForm : DIForm
    {
        private readonly DbContext _dbContext;
        private readonly Type _etype;
        private readonly ISchemaManager _schemaManager;
        private int _count;
        private IList _data;
        private Action<int> _delete;
        private Action<int> _detail, _edit;
        private bool _hasEditColumn;
        private Task<IList> _loadTask;
        private readonly CancellationTokenSource _taskCancellationTokenSource;

        public ListForm(IDIKernel diKernel, Type etype) : base(diKernel)
        {
            InitializeComponent();
            _taskCancellationTokenSource = new CancellationTokenSource();
            ydgvList.RowHeadersVisible = false;
            _etype = etype;
            _dbContext = Get<DbContext>();
            _schemaManager = Get<ISchemaManager>();
            Text = etype.GetDisplayName();
            _hasEditColumn = false;
            InitializeData();
            InitializeButtons();
            InitializeActions();
            ydgvList.CellClick += YdgvListCellClick;
        }

        private void InitializeButtons()
        {
            var addButton = new ToolStripButton("Новый");
            addButton.Click += (sender, e) => new EditForm(DIKernel, _etype, -1) {MdiParent = MdiParent}.Show();
            var refreshButton = new ToolStripButton("Обновить");
            refreshButton.Click += (sender, e) =>
                                       {
                                           InitializeData();
                                           InitializeColumns();
                                       };
            toolPanel.Items.AddRange(new[] {addButton, refreshButton});
        }

        private void InitializeActions()
        {
            _detail = DetailAction;
            _edit = EditAction;
            _delete = DeleteAction;
        }

        private void DeleteAction(int key)
        {
            try
            {
                object obj = _data.FirstOrDefault(_etype, o =>
                                                  Convert.ToInt32(_schemaManager.Key(_etype).GetValue(o, null)) == key);
                _dbContext.Entry(obj).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                YMessageBox.Information("Удалено.");
            }
            catch (Exception e)
            {
                throw new Exception("Невозможно удалить запись.", e);
            }
        }

        private void EditAction(int key)
        {
            var form = new EditForm(DIKernel, _etype, key) {MdiParent = MdiParent};
            form.Show();
        }

        private void DetailAction(int key)
        {
            MessageBox.Show(string.Format("Details for {0}", key));
        }

        private void YdgvListCellClick(object sender, DataGridViewCellEventArgs e)
        {
            int key = Convert.ToInt32(_schemaManager.Key(_etype).GetValue(_data[e.RowIndex], null));
            switch (ydgvList.Columns[e.ColumnIndex].Name)
            {
                case "ActionDetail":
                    {
                        _detail(key);
                        break;
                    }
                case "ActionEdit":
                    {
                        _edit(key);
                        break;
                    }
                case "ActionDelete":
                    {
                        if (!Get<DIMainForm>().MdiChildren.Any(f => f is DIForm && (f as DIForm).UID == _etype.FullName + key))
                        {
                            if (MessageBox.Show(@"Точно хотите удалить?", @"Подтверждение", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                _delete(key);
                                ydgvList.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                        else
                        {
                            YMessageBox.Information(
                                "Имеются открытые формы для редактирования. Закройте их перед удаление записи.");
                        }
                        break;
                    }
            }
        }

        private void InitializeData()
        {
            loadControl.Show();
            _loadTask =
                new Task<IList>(
                    () => _data = ((IQueryable<object>) _dbContext.Set(_etype)).ToList(_etype), _taskCancellationTokenSource.Token);
            _loadTask.ContinueWith(t =>
                                  {
                                      if (!_taskCancellationTokenSource.IsCancellationRequested)
                                      {
                                          _count = t.Result.Count;
                                          Invoke(new MethodInvoker(() =>
                                                                       {
                                                                           ydgvList.BindingSource = new BindingSource { DataSource = t.Result };
                                                                           InitializeColumns();
                                                                           loadControl.Hide();
                                                                       }));
                                      }
                                  });
            _loadTask.Start();
        }

        private void InitializeColumns()
        {
            if (!_hasEditColumn && _schemaManager.HasKey(_etype) && _count > 0)
            {
                var detailColumn = new DataGridViewButtonColumn
                                       {
                                           AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                                           HeaderText = @"Детали",
                                           Name = @"ActionDetail",
                                           Text = @"Детали",
                                           UseColumnTextForButtonValue = true
                                       };
                var editColumn = new DataGridViewButtonColumn
                                     {
                                         AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                                         HeaderText = @"Ред.",
                                         Name = @"ActionEdit",
                                         Text = @"Ред.",
                                         UseColumnTextForButtonValue = true
                                     };
                var deleteColumn = new DataGridViewButtonColumn
                                       {
                                           AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                                           HeaderText = @"Удалить",
                                           Name = @"ActionDelete",
                                           Text = @"Удалить",
                                           UseColumnTextForButtonValue = true
                                       };
                ydgvList.Columns.AddRange(new[] {detailColumn, editColumn, deleteColumn});
                _hasEditColumn = true;
            }
        }

        private void ListFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_loadTask.Status == TaskStatus.Running && _taskCancellationTokenSource.Token.CanBeCanceled)
                _taskCancellationTokenSource.Cancel();
        }
    }
}