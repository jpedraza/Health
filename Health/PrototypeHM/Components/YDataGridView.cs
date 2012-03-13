using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFCFModel.Attributes;

namespace PrototypeHM.Components
{
    public enum XLoadMode
    {
        Synchronize,
        Asynchronize
    }

    public class YDataGridView : DataGridView
    {
        private bool _convertEmptyStringToNull;
        private readonly SynchronizationContext _synchronizationContext;
        private readonly Task _loadTask;
        private readonly CancellationTokenSource _tokenSource;

        public YDataGridView()
        {
            _synchronizationContext = SynchronizationContext.Current;
            _tokenSource = new CancellationTokenSource();
            _loadTask = new Task(() =>
                                     {
                                         BindingSource source = LoadDataAction();
                                         _synchronizationContext.Post(c => BindingSource = source, null);
                                     }, _tokenSource.Token);
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = Color.White;
            Disposed += YDataGridViewDisposed;
        }

        private void YDataGridViewDisposed(object sender, EventArgs e)
        {
            if (_loadTask.Status == TaskStatus.Running && _tokenSource.Token.CanBeCanceled)
                _tokenSource.Cancel();
        }

        public BindingSource BindingSource
        {
            get { return (DataSource as BindingSource); }
            set { DataSource = value; }
        }

        public XLoadMode LoadMode { get; set; }

        public Func<BindingSource> LoadDataAction { get; set; }

        public void LoadData()
        {
            switch (LoadMode)
            {
                case XLoadMode.Asynchronize:
                    {
                        _loadTask.Start();
                        break;
                    }
                    case XLoadMode.Synchronize:
                    {
                        BindingSource = LoadDataAction();
                        break;
                    }
            }
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            FixAutoGenerateColumn();
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            FixAutoGenerateColumn();
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (_convertEmptyStringToNull && e.Value != null && string.IsNullOrEmpty(e.Value.ToString()))
            {
                Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                e.FormattingApplied = true;
            }
        }

        private void FixAutoGenerateColumn()
        {
            object source = BindingSource != null
                                ? BindingSource.DataSource
                                : BindingSource;
            if (source != null && source.GetType().IsGenericType && source.GetType().GetGenericArguments().Any())
            {
                Type[] genericArguments = source.GetType().GetGenericArguments();
                Type objType = genericArguments[0];
                var metadataTypeAttribute =
                    objType.GetCustomAttributes(true).FirstOrDefault(a => a is MetadataTypeAttribute) as
                    MetadataTypeAttribute;
                if (metadataTypeAttribute != null)
                {
                    PropertyInfo[] propertiesInfos = metadataTypeAttribute.MetadataClassType.GetProperties();
                    ProcessDisplayAttribute(propertiesInfos);
                }
                PropertyInfo[] propertiesInfo = objType.GetProperties();
                ProcessDisplayAttribute(propertiesInfo);
            }
        }

        private void ProcessDisplayAttribute(IEnumerable<PropertyInfo> propertiesInfo)
        {
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                DataGridViewColumn column = Columns[propertyInfo.Name];
                if (column != null)
                {
                    object[] attributes = propertyInfo.GetCustomAttributes(true);
                    foreach (object att in attributes)
                    {
                        if (att is DisplayNameAttribute)
                        {
                            column.HeaderText = (att as DisplayNameAttribute).DisplayName;
                            continue;
                        }
                        if (att is NotDisplayAttribute)
                        {
                            Columns.Remove(propertyInfo.Name);
                            continue;
                        }
                        if (att is HideAttribute)
                        {
                            column.Visible = false;
                            continue;
                        }
                        if (att is DisplayFormatAttribute)
                        {
                            _convertEmptyStringToNull = (att as DisplayFormatAttribute).ConvertEmptyStringToNull;
                            column.DefaultCellStyle.Format = (att as DisplayFormatAttribute).DataFormatString;
                            column.DefaultCellStyle.NullValue = (att as DisplayFormatAttribute).NullDisplayText;
                        }
                    }
                }
            }
        }
    }
}