using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrototypeHM.DI;

namespace PrototypeHM.Components
{
    public enum LoadMode
    {
        Sync,
        Async
    }

    /// <summary>
    /// Компонент обеспечивает миграцию однотипных объектов между двумя источниками данных.
    /// </summary>
    public partial class MultiSelector : UserControl, IDIInjected
    {
        /// <summary>
        /// Делегат события при перемещении элементов.
        /// </summary>
        /// <param name="objs">Перемещаемые элементы.</param>
        /// <returns>Разрешить перемещение или нет?</returns>
        public delegate bool EventMove(IBindingList objs);

        public EventMove OnBeforeMoveToRight { get; set; }

        public EventMove OnBeforeMoveToLeft { get; set; }

        private bool _editMode;

        public BindingSource LeftSource { get; private set; }

        public BindingSource RightSource { get; private set; }

        public IDIKernel DIKernel { get; private set; }

        /// <summary>
        /// Задать или узнать включен ли режим редактирования.
        /// </summary>
        private bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                controlPanel.Visible = _editMode;
                splitContainer.Panel2Collapsed = !_editMode;
                if (_editMode)
                {
                    splitContainer.Panel1MinSize = splitContainer.Width / 2 - 5;
                    splitContainer.Panel2MinSize = splitContainer.Width / 2 - 5;
                    splitContainer.SplitterDistance = splitContainer.Width / 2 - 5;
                }
            }
        }

        public Func<BindingSource> LeftLoad { get; set; }

        public Func<BindingSource> RightLoad { get; set; }

        private Task _loadTask;
        private readonly CancellationTokenSource _tokenSource;

        public MultiSelector(IDIKernel diKernel)
        {
            _tokenSource = new CancellationTokenSource();
            Disposed += MultiSelectorDisposed;
            DIKernel = diKernel;
            InitializeComponent();
            EditMode = false;
        }

        private void MultiSelectorDisposed(object sender, EventArgs e)
        {
            if (_loadTask != null && _loadTask.Status == TaskStatus.Running && _tokenSource.Token.CanBeCanceled)
                _tokenSource.Cancel();
        }

        private void BtnDisplayModeClick(object sender, EventArgs e)
        {
            EditMode = !EditMode;
        }

        public void LoadData(LoadMode loadMode)
        {
            HandleCreated += (sender, e) => M(loadMode);
        }

        private void M(LoadMode loadMode)
        {
            if (LeftLoad == null)
                throw new NullReferenceException("Метод для загрузки левых данных не указан.");

            if (RightLoad == null)
                throw new NullReferenceException("Метод для загрузки правых данных не указан.");

            if (loadMode == LoadMode.Sync)
            {
                BindingSource left = LeftLoad();
                BindingSource right = RightLoad();
                SetData(left, right);
            }
            else if (loadMode == LoadMode.Async)
            {
                leftLoadControl.Show();
                rightLoadControl.Show();
                _loadTask = new Task(() => {
                    BindingSource left = LeftLoad();
                    BindingSource right = RightLoad();
                    Invoke((Action)(() => SetData(left, right)));
                }, _tokenSource.Token);
                _loadTask.Start();
            }
        }

        /// <summary>
        /// Установить источники данных.
        /// Источник данных автоматически упаковывается в BindingSource, 
        /// либо возможно передать уже упакованный объект, тогда повторная 
        /// упаковка не произойдет.
        /// </summary>
        /// <param name="left">Левый.</param>
        /// <param name="right">Правый.</param>
        private void SetData(BindingSource left, BindingSource right)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            if (right == null)
                throw new ArgumentNullException("right");

            if (left.DataSource.GetType() != right.DataSource.GetType())
                throw new Exception("Источники данных должны быть одного типа.");

            btnToLeft.Enabled = left.Count > 0;
            btnToRight.Enabled = right.Count > 0;

            LeftSource = left;
            RightSource = right;
            ydgvLeft.BindingSource = LeftSource;
            leftLoadControl.Hide();
            ydgvRight.BindingSource = RightSource;
            rightLoadControl.Hide();

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