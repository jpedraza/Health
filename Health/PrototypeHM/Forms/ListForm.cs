using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Windows.Forms;
using EFCFModel;
using PrototypeHM.DI;

namespace PrototypeHM.Forms
{
    public partial class ListForm : DIForm
    {
        private readonly DbContext _dbContext;
        private readonly Type _etype;
        private readonly SchemaManager _schemaManager;
        private int _count;
        private IList _data;
        private Action<int> _delete;
        private Action<int> _detail, _edit;
        private bool _hasEditColumn;

        public ListForm(IDIKernel diKernel, Type etype) : base(diKernel)
        {
            InitializeComponent();
            ydgvList.RowHeadersVisible = false;
            _etype = etype;
            _dbContext = Get<DbContext>();
            _schemaManager = Get<SchemaManager>();
            Text = etype.GetDisplayName();
            _hasEditColumn = false;
            InitializeData();
            InitializeColumns();
            InitializeButtons();
            InitializeActions();
            ydgvList.CellClick += YdgvListCellClick;
        }

        private void InitializeButtons()
        {
            var addButton = new ToolStripButton("Новый");
            addButton.Click += (sender, e) => new EditForm(DIKernel, _etype, -1) { MdiParent = MdiParent }.Show();
            var refreshButton = new ToolStripButton("Обновить");
            refreshButton.Click += (sender, e) =>
                                       {
                                           InitializeData();
                                           InitializeColumns();
                                       };
            toolPanel.Items.AddRange(new[] { addButton, refreshButton });
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
                object obj = _data.FirstOrDefault(_etype,
                                                  o =>
                                                  Convert.ToInt32(_schemaManager.Key(_etype).GetValue(o, null)) ==
                                                  key);
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
                        if (MessageBox.Show(@"Точно хотите удалить?", @"Подтверждение", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _delete(key);
                            ydgvList.Rows.RemoveAt(e.RowIndex);
                        }
                        break;
                    }
            }
        }

        private void InitializeData()
        {
            _data = ((IEnumerable<object>) _dbContext.Set(_etype)).ToListOfObjects(_etype);
            _count = _data.Count;
            ydgvList.BindingSource = new BindingSource {DataSource = _data};
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
                ydgvList.Columns.AddRange(new[] { detailColumn, editColumn, deleteColumn });
                _hasEditColumn = true;
            }
        }
    }
}