using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
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

        #region Implementation of IDIInjected

        public IDIKernel DIKernel { get; private set; }

        #endregion

        public SingleSelector(IDIKernel diKernel, Type etype)
        {
            InitializeComponent();
            DIKernel = diKernel;
            _etype = etype;
            _dbContext = DIKernel.Get<DbContext>();
            _expand = false;
            InitializeData();
            Width = 275;
            ydgvCollection.Visible = false;
            ydgvCollection.RowHeadersVisible = false;
        }

        public object SelectedData
        {
            get { return _selectedData; }
            set
            {
                if (value != null)
                {
                    if (!_etype.IsAssignableFrom(value.GetType()))
                        throw new Exception(string.Format("Value type must be {0}, but get {1}.", _etype.FullName,
                                                          value.GetType().FullName));
                    _selectedData = value;
                    txbSelectedValue.Text = value.ToString();
                }
            }
        }

        public string PropertyName { get; set; }
        public Action<object> ValueChange { get; set; }

        public void InitializeData()
        {
            _data = ((IQueryable<object>) _dbContext.Set(_etype)).ToList(_etype);
            ydgvCollection.BindingSource = new BindingSource {DataSource = _data};
        }

        private void YdgvCollectionCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_data != null)
            {
                SelectedData = _data[e.RowIndex];
                ExpandTop();
                if (ValueChange != null) ValueChange(SelectedData);
            }
        }

        private void ExpandTop()
        {
            _expand = false;
            ydgvCollection.Visible = false;
            Width = 275;
            Height = 20;
        }

        private void ExpandBottom()
        {
            _expand = true;
            ydgvCollection.Visible = true;
            Width = 400;
            if (0 < ydgvCollection.RowCount && ydgvCollection.RowCount < 10)
            {
                ydgvCollection.Height = ydgvCollection.Rows[0].Height*ydgvCollection.RowCount +
                                        ydgvCollection.ColumnHeadersHeight + 1;
                Height = ydgvCollection.Height + txbSelectedValue.Height + 1;
            }
            else
            {
                Height = 400;
            }
        }

        private void BtnSelectClick(object sender, EventArgs e)
        {
            if (_expand)
            {
                ExpandTop();
            }
            else
            {
                ExpandBottom();
            }
        }
    }
}