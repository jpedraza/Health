using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrototypeHM.DI;

namespace PrototypeHM.Components
{
    public partial class SingleSelector : UserControl, IDIInjected
    {
        private readonly DbContext _dbContext;
        private readonly Type _etype;
        private IList _data;
        private bool _expand;
        private object _selectedData;
        private Task<IList> _loadTask;
        private readonly CancellationTokenSource _loadCancellationTokenSource;
        private readonly SynchronizationContext _synchronizationContext;

        public IDIKernel DIKernel { get; private set; }

        public SingleSelector(IDIKernel diKernel, Type etype)
        {
            Disposed += SingleSelectorDisposed;
            InitializeComponent();
            DIKernel = diKernel;
            _loadCancellationTokenSource = new CancellationTokenSource();
            _synchronizationContext = SynchronizationContext.Current;
            _etype = etype;
            _dbContext = DIKernel.Get<DbContext>();
            ydgvCollection.RowHeadersVisible = false;
            ExpandTop();
        }

        private void SingleSelectorDisposed(object sender, EventArgs e)
        {
            if (_loadCancellationTokenSource.Token.CanBeCanceled)
                _loadCancellationTokenSource.Cancel();
        }

        public object SelectedData
        {
            get { return _selectedData; }
            set
            {
                if (value != null)
                {
                    if (!_etype.IsInstanceOfType(value))
                        throw new Exception(string.Format("Value type must be {0}, but get {1}.", _etype.FullName,
                                                          value.GetType().FullName));
                    _selectedData = value;
                    txbSelectedValue.Text = value.ToString();
                }
            }
        }

        public Action<object> ValueChange { get; set; }

        private void InitializeData()
        {
            loadControl.Show();
            _loadTask = new Task<IList>(() => _data = ((IQueryable<object>)_dbContext.Set(_etype)).ToList(_etype));
            _loadTask.ContinueWith(task => _synchronizationContext.Post(c =>
                                                                            {
                                                                                ydgvCollection.BindingSource =
                                                                                    new BindingSource
                                                                                        {DataSource = _data};
                                                                                loadControl.Hide();
                                                                            }, null), TaskContinuationOptions.OnlyOnRanToCompletion);
            _loadTask.Start();
        }

        private void YdgvCollectionCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_data != null && e.RowIndex >= 0)
            {
                SelectedData = _data[e.RowIndex];
                ExpandTop();
                if (ValueChange != null) ValueChange(SelectedData);
            }
        }

        private void ExpandTop()
        {
            Height = Convert.ToInt32(tableLayoutPanel.RowStyles[0].Height);
            _expand = false;
            panel.Visible = false;
        }

        private void ExpandBottom()
        {
            Height = 400;
            _expand = true;
            panel.Visible = true;
        }

        private void ExpandClick(object sender, EventArgs e)
        {
            if (_expand)
            {
                SingleSelectorDisposed(sender, e);
                ExpandTop();
            }
            else
            {
                InitializeData();
                ExpandBottom();
            }
        }
    }
}