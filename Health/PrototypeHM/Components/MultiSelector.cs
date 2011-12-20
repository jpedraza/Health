using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrototypeHM.Components
{
    public partial class MultiSelector : UserControl
    {
        public object LeftData { get { return ydgvLeft.DataSource; } }

        public object RightData { get { return ydgvRight.DataSource; } }

        public Action<IList<object>, MultiSelector> LeftToRightMove { get; set; }

        public Action<IList<object>, MultiSelector> RightToLeftMove { get; set; }

        public MultiSelector()
        {
            InitializeComponent();
        }

        private IList<object> LeftSource { get; set; }
        private IList<object> RightSource { get; set; }

        private void BtnDisplayModeClick(object sender, EventArgs e)
        {
            Width = Width == 300 ? 725 : 300;
        }

        public void SetData(IList<object> left, IList<object> right)
        {
            LeftSource = left;
            RightSource = right;
            if (left == null && right != null)
            {
                LeftSource = (Activator.CreateInstance(right.GetType()) as IList).ToListOfObjects();
            }
            if (right == null && left != null)
            {
                RightSource = (Activator.CreateInstance(left.GetType()) as IList).ToListOfObjects();
            }
            ydgvLeft.DataSource = LeftSource;
            ydgvRight.DataSource = RightSource;
        }

        private void BtnToRightClick(object sender, EventArgs e)
        {
            var objs = new List<object>();
            for (int i = 0; i < ydgvLeft.SelectedRows.Count; i++)
            {
                objs.Add(LeftSource[i]);       
            }
            LeftToRightMove(objs, this);
        }

        private void BtnToLeftClick(object sender, EventArgs e)
        {
            var objs = new List<object>();
            for (int i = 0; i < ydgvRight.SelectedRows.Count; i++)
            {
                objs.Add(RightSource[i]);
            }
            RightToLeftMove(objs, this);
        }
    }
}
