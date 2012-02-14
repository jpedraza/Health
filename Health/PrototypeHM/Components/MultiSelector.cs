using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace PrototypeHM.Components
{
    /// <summary>
    /// Компонент обеспечивает миграцию однотипных объектов между двумя источниками данных.
    /// </summary>
    public partial class MultiSelector : UserControl
    {
        public MultiSelector()
        {
            InitializeComponent();
            EditMode = false;
        }

        private  bool _editMode;
        /// <summary>
        /// Задать или узнать включен ли режим редактирования.
        /// </summary>
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                controlPanel.Visible = _editMode;
                splitContainer.Panel2Collapsed = !_editMode;
                if (_editMode)
                {
                    splitContainer.Panel1MinSize = splitContainer.Width/2 - 5;
                    splitContainer.Panel2MinSize = splitContainer.Width/2 - 5;
                    splitContainer.SplitterDistance = splitContainer.Width/2 - 3;
                }
            }
        }

        /// <summary>
        /// Делегат события при перемещении элементов.
        /// </summary>
        /// <param name="objs">Перемещаемые элементы.</param>
        /// <returns>Разрешить перемещение или нет?</returns>
        public delegate bool EventMove(IBindingList objs);

        /// <summary>
        /// Событие возникате перед перемещением элемента из левого источника данных в правый.
        /// </summary>
        public EventMove OnBeforeMoveToRight;

        /// <summary>
        /// Событие возникате перед перемещением элемента из правого источника данных в левый.
        /// </summary>
        public EventMove OnBeforeMoveToLeft;

        /// <summary>
        /// Левый источник данных.
        /// </summary>
        public BindingSource LeftSource { get; private set; }

        /// <summary>
        /// Правый источник данных.
        /// </summary>
        public BindingSource RightSource { get; private set; }

        private void BtnDisplayModeClick(object sender, EventArgs e)
        {
            EditMode = !EditMode;
        }

        /// <summary>
        /// Установить источники данных.
        /// Источник данных автоматически упаковывается в BindingSource, 
        /// либо возможно передать уже упакованный объект, тогда повторная 
        /// упаковка не произойдет.
        /// </summary>
        /// <param name="left">Левый.</param>
        /// <param name="right">Правый.</param>
        public void SetData(object left, object right)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            if (right == null)
                throw new ArgumentNullException("right");

            BindingSource leftBindingSource = left.GetType().IsAssignableFrom((typeof (BindingSource)))
                                                  ? (BindingSource) left
                                                  : new BindingSource {DataSource = left};
            BindingSource rightBindingSource = right.GetType().IsAssignableFrom(typeof (BindingSource))
                                                   ? (BindingSource) right
                                                   : new BindingSource {DataSource = right};

            if (leftBindingSource.DataSource.GetType() != rightBindingSource.DataSource.GetType())
                throw new Exception("Источники данных должны быть одного типа.");

            if (leftBindingSource.Count == 0 && rightBindingSource.Count == 0)
                throw new Exception("Число элементов хотя бы в одной из коллекций должно быть больше 0.");

            btnToLeft.Enabled = leftBindingSource.Count == 0;
            btnToRight.Enabled = rightBindingSource.Count == 0;

            LeftSource = leftBindingSource;
            RightSource = rightBindingSource;
            ydgvLeft.DataSource = LeftSource;
            ydgvRight.DataSource = RightSource;

            LeftSource.ListChanged += LeftSourceListChanged;
            RightSource.ListChanged += RightSourceListChanged;
        }

        private void RightSourceListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemAdded)
                btnToLeft.Enabled = RightSource.Count != 0;
        }

        private void LeftSourceListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemAdded)
                btnToRight.Enabled = LeftSource.Count != 0;
        }

        private void BtnToRightClick(object sender, EventArgs e)
        {
            var objs = new BindingList<object>();
            var rows = new List<int>();
            for (int i = 0; i < ydgvLeft.SelectedCells.Count; i++)
            {
                DataGridViewCell cell = ydgvLeft.SelectedCells[i];
                if (!rows.Contains(cell.RowIndex))
                {
                    rows.Add(cell.RowIndex);
                    objs.Add(LeftSource[cell.RowIndex]);
                }
            }
            if (OnBeforeMoveToRight != null && OnBeforeMoveToRight(objs))
                MoveToRight(objs);
            else 
                MoveToRight(objs);
        }

        private void MoveToRight(IBindingList objs)
        {
            RightSource.AddRange(objs);
            LeftSource.RemoveRange(objs);
        }

        private void BtnToLeftClick(object sender, EventArgs e)
        {
            var objs = new BindingList<object>();
            var rows = new List<int>();
            for (int i = 0; i < ydgvRight.SelectedCells.Count; i++)
            {
                DataGridViewCell cell = ydgvRight.SelectedCells[i];
                if (!rows.Contains(cell.RowIndex))
                {
                    rows.Add(cell.RowIndex);
                    objs.Add(RightSource[cell.RowIndex]);
                }
            }
            if (OnBeforeMoveToLeft != null && OnBeforeMoveToLeft(objs))
                MoveToLeft(objs);
            else 
                MoveToLeft(objs);
        }

        private void MoveToLeft(IBindingList objs)
        {
            LeftSource.AddRange(objs);
            RightSource.RemoveRange(objs);
        }
    }
}
