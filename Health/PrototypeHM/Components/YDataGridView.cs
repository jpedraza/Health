using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EFCFModel.Attributes;

namespace PrototypeHM.Components
{
    public class YDataGridView : DataGridView
    {
        private bool _convertEmptyStringToNull;

        public YDataGridView()
        {
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = Color.White;
            CellClick += CellClickEvent;
        }

        public BindingSource BindingSource
        {
            get { return (DataSource as BindingSource); }
            set { DataSource = value; }
        }

        public Action<int> Detail { get; set; }

        public Action<int> Delete { get; set; }

        public void SetDataSource(BindingSource dataSource)
        {
            BindingSource = dataSource;
        }

        private void CellClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            if (Detail != null && Columns[e.ColumnIndex].Name == @"Детали")
            {
                Detail(e.RowIndex);
                return;
            }
            if (Delete != null && Columns[e.ColumnIndex].Name == @"Удалить")
            {
                Delete(e.RowIndex);
                return;
            }
        }

        public void InitializeOperations()
        {
            if (Detail != null)
            {
                var column = new DataGridViewButtonColumn
                                 {
                                     Text = @"Детали",
                                     Name = @"Детали",
                                     UseColumnTextForButtonValue = true
                                 };
                Columns.Add(column);
            }
            if (Delete != null)
            {
                var column = new DataGridViewButtonColumn
                                 {
                                     Text = @"Удалить",
                                     Name = @"Удалить",
                                     UseColumnTextForButtonValue = true
                                 };
                Columns.Add(column);
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
                Type st = source.GetType();
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
                            continue;
                        }
                    }
                }
            }
        }
    }
}