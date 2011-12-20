using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrototypeHM.Components
{
    public partial class SingleSelector<T> : UserControl
        where T : class, new()
    {
        public Func<IList<T>> LoadData { get; set; }

        public Action<T> ValueChange { get; set; }

        public string SourceProperty { get; set; }

        private bool _expand;

        private IList<T> _data;

        public SingleSelector()
        {
            _expand = false;
            InitializeComponent();
            Width = 275;
            ydgvCollection.Visible = false;
            ydgvCollection.RowHeadersVisible = false;
        }

        public void InitializeData()
        {
            if (LoadData != null)
            {
                _data = LoadData();
                ydgvCollection.DataSource = _data;
            }
        }

        private void YdgvCollectionCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_data != null && !string.IsNullOrEmpty(SourceProperty))
            {
                var value =
                    _data[e.RowIndex].GetType().GetProperty(SourceProperty).GetValue(_data[e.RowIndex], null) as string;
                txbSelectedValue.Text = value;
                ExpandTop();
                if (ValueChange != null) ValueChange(_data[e.RowIndex]);
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
            if (ydgvCollection.RowCount < 10)
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
