using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using PrototypeHM.Components;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using PrototypeHM.DB.DI;
using System.Windows.Forms;
using PrototypeHM.Doctor;

namespace PrototypeHM.Forms
{
    public partial class AddForm<TData> : DIForm
        where TData : class, new()
    {
        private TData _dataObject;

        public Func<TData, QueryStatus> SaveData { get; set; }

        private int _cHeight = 15;
        private const int LHeight = 24;

        public AddForm()
        {
            BaseConstructor();
        }

        public AddForm(IDIKernel diKernel) : base(diKernel)
        {
            BaseConstructor();
        }

        private void BaseConstructor()
        {
            _dataObject = Get<DoctorRepository>().Detail(Get<DoctorRepository>().GetAll().First(d => d.Id == 12)) as TData;
            InitializeComponent();
        }

        public void InitializeForm()
        {
            Type dataType = typeof (TData);
            PropertyInfo[] propertiesInfo = dataType.GetProperties();
            if (SaveData != null)
            {
                var saveC = new ToolStripButton {DisplayStyle = ToolStripItemDisplayStyle.Text, Text = @"Сохранить"};
                saveC.Click += (sender, e) =>
                                   {
                                       QueryStatus status = SaveData(_dataObject);
                                       YMessageBox.Information(status.StatusMessage);
                                   };
                tsOperations.Items.Add(saveC);
            }
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                if (propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                        a => a.GetType() == typeof (NotDisplayAttribute) || a.GetType() == typeof (HideAttribute)) == null)
                {
                    string textForLabel = propertyInfo.Name;
                    var displayNameAttribute =
                        propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                            a => a.GetType() == typeof (DisplayNameAttribute)) as DisplayNameAttribute;
                    if (displayNameAttribute != null) textForLabel = displayNameAttribute.DisplayName;
                    var labelText = new Label {Text = textForLabel, Height = LHeight, Top = _cHeight, Left = 15};
                    Control c = null;
                    var singSelectAtt =
                        propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                            a => a.GetType() == typeof (SingleSelectEditModeAttribute)) as SingleSelectEditModeAttribute;
                    if (singSelectAtt != null && singSelectAtt.OperationContext != null)
                    {
                        object operationContext = Get<OperationsRepository>().Operations.FirstOrDefault(
                            o => o.GetType() == singSelectAtt.OperationContext);
                        if (operationContext != null)
                        {
                            Type funcType =
                                typeof (Func<>).MakeGenericType(
                                    typeof (IList<>).MakeGenericType(
                                        singSelectAtt.OperationContext.GetGenericArguments()[0]));
                            if (operationContext.GetType().GetProperty("Load").PropertyType == funcType)
                            {
                                object loadProperty =
                                    operationContext.GetType().GetProperty("Load").GetValue(operationContext, null);
                                if (loadProperty != null)
                                {
                                    Type singleSelectorType =
                                        typeof (SingleSelector<>).MakeGenericType(
                                            singSelectAtt.OperationContext.GetGenericArguments()[0]);
                                    c = Activator.CreateInstance(singleSelectorType) as Control;
                                    singleSelectorType.GetProperty("Top").SetValue(c, _cHeight, null);
                                    singleSelectorType.GetProperty("Left").SetValue(c, labelText.Width + labelText.Left, null);
                                    object txbSv = singleSelectorType.GetField("txbSelectedValue").GetValue(c);
                                    object databindings = txbSv.GetType().GetProperty("DataBindings").GetValue(txbSv, null);
                                    databindings.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null,
                                                                        databindings,
                                                                        new object[]
                                                                            {
                                                                                "Text", _dataObject,
                                                                                propertyInfo.Name,
                                                                                false,
                                                                                DataSourceUpdateMode.OnPropertyChanged
                                                                            });
                                    singleSelectorType.GetProperty("SourceProperty").SetValue(c, singSelectAtt.SourceProperty, null);
                                    singleSelectorType.GetProperty("LoadData").SetValue(c, loadProperty, null);
                                    singleSelectorType.GetMethod("InitializeData").Invoke(c, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        var editModeAtt =
                            propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                                a => a.GetType() == typeof(EditModeAttribute)) as EditModeAttribute;
                        if (propertyInfo.PropertyType == typeof (string))
                        {
                            c = new TextBox {Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left};
                            c.DataBindings.Add("Text", _dataObject, propertyInfo.Name, false,
                                               DataSourceUpdateMode.OnPropertyChanged);
                            if (editModeAtt != null && editModeAtt.Mode.HasFlag(EditModeEnum.Multiline))
                            {
                                (c as TextBox).Multiline = true;
                                (c as TextBox).ScrollBars = ScrollBars.Both;
                                c.Height = c.Height*3;
                                c.Width = c.Width*3;
                            }
                        }
                        if (propertyInfo.PropertyType == typeof (DateTime))
                        {
                            c = new DateTimePicker
                                    {Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left};
                            c.DataBindings.Add("Value", _dataObject, propertyInfo.Name, false,
                                               DataSourceUpdateMode.OnPropertyChanged);
                        }
                        if (propertyInfo.PropertyType == typeof (int))
                        {
                            c = new NumericUpDown
                                    {Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left};
                            c.DataBindings.Add("Value", _dataObject, propertyInfo.Name, false,
                                               DataSourceUpdateMode.OnPropertyChanged);
                        }
                        if (propertyInfo.PropertyType.Name == typeof (ICollection<>).Name)
                        {
                            c = new YDataGridView
                                    {Height = 200, Top = _cHeight, Left = labelText.Width + labelText.Left, Width = 400};
                            (c as YDataGridView).DataSource = propertyInfo.GetValue(_dataObject, null);
                            (c as YDataGridView).RowHeadersVisible = false;
                        }
                    }
                    if (c != null)
                    {
                        if (propertyInfo.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof (NotEditAttribute)) != null)
                        {
                            c.Enabled = false;
                        }
                        _cHeight += c.Height + 5;
                        tscContent.ContentPanel.Controls.AddRange(new[] {c, labelText});
                    }
                }
            }
        }
    }
}
