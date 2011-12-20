using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM.Components
{
    public class YDataGridView : DataGridView
    {
        public YDataGridView()
        {
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = Color.White;
            CellClick += CellClickEvent;
        }

        private bool _convertEmptyStringToNull;

        public Action<int> Detail { get; set; }

        public Action<int> Delete { get; set; }

        private void CellClickEvent(object sender, DataGridViewCellEventArgs e)
        {
            if (Detail != null && Columns[e.ColumnIndex].Name == @"Детали") { Detail(e.RowIndex); return; }
            if (Delete != null && Columns[e.ColumnIndex].Name == @"Удалить") { Delete(e.RowIndex); return; }
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
            var source = DataSource as IEnumerable<object>;
            if (source == null) return;
            Type[] genericArguments = source.GetType().GetGenericArguments();
            if (genericArguments.Length != 1) return;
            Type objType = genericArguments[0];
            PropertyInfo[] propertiesInfo = objType.GetProperties();
            ProcessDisplayAttribute(propertiesInfo);
        }

        private void ProcessDisplayAttribute(IEnumerable<PropertyInfo> propertiesInfo)
        {
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                DataGridViewColumn column = Columns[propertyInfo.Name];
                if (column != null)
                {
                    object[] attributes = propertyInfo.GetCustomAttributes(true);
                    if (attributes.Where(a => a.GetType() == typeof(NotDisplayAttribute)).FirstOrDefault() != null)
                    {
                        Columns.Remove(propertyInfo.Name);
                    }
                    if (attributes.Where(a => a.GetType() == typeof(HideAttribute)).FirstOrDefault() != null)
                    {
                        column.Visible = false;
                    }
                    var attribute =
                        attributes.Where(a => a.GetType() == typeof(DisplayFormatAttribute)).FirstOrDefault() as
                        DisplayFormatAttribute;
                    if (attribute != null)
                    {
                        _convertEmptyStringToNull = attribute.ConvertEmptyStringToNull;
                        column.DefaultCellStyle.Format = attribute.DataFormatString;
                        column.DefaultCellStyle.NullValue = attribute.NullDisplayText;
                    }
                }
            }
        }
    }
}
