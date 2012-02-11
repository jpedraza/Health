using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PrototypeHM.Components;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using PrototypeHM.DB.DI;
using PrototypeHM.Properties;

namespace PrototypeHM.Forms
{
    public partial class AddForm<TData> : DIForm
        where TData : class, IIdentity, new()
    {
        //Необходим открытый доступ к данному св-ву,
        //для возможности реализации вложенности
        private const int LHeight = 24;

        private readonly int _Id;
        private int _cHeight = 15;
        private IList<PropertyInfo> _collectionsPropertiInfos;
        public TData _dataObject;
        private IList<YDataGridView> _dgvs;

        public AddForm()
        {
            BaseConstructor();
        }

        public AddForm(IDIKernel diKernel)
            : base(diKernel)
        {
            BaseConstructor();
        }

        public AddForm(IDIKernel diKernel, int Id) : base(diKernel)
        {
            _Id = Id;
            BaseConstructor();
        }

        public Func<TData, QueryStatus> SaveData { get; set; }

        public Func<TData, object> DetailData { get; set; }

        public Func<TData, QueryStatus> UpdateData { get; set; }

        private void BaseConstructor()
        {
            InitializeComponent();
        }

        /*
         * Создаем служебный список, хранящий все таблицы, хранящие коллекции 
         * отображаемого типа данных
         */

        public void InitializeForm()
        {
            _dgvs = new List<YDataGridView>();
            _collectionsPropertiInfos = new List<PropertyInfo>();

            Type dataType = typeof (TData);
            PropertyInfo[] propertiesInfo = dataType.GetProperties();

            var operationsContext =
                DIKernel.Get<OperationsRepository>().Operations.FirstOrDefault(o => o.GetType() == typeof (OperationsContext<TData>)) as
                OperationsContext<TData>;

            if (operationsContext != null)
            {
                if (_Id == -1)
                {
                    SaveData = operationsContext.Save;
                    _dataObject = new TData();
                }
                else
                {
                    UpdateData = operationsContext.Update;
                    DetailData = operationsContext.Detail;
                }
            }

            if (SaveData != null)
            {
                var saveC = new ToolStripButton
                                {
                                    DisplayStyle = ToolStripItemDisplayStyle.Text,
                                    Text = Resources.AddForm_InitializeForm_Сохранить
                                };
                saveC.Click += delegate
                                   {
                                       //Флаг, показывающий - нормально ли сохранились данные
                                       bool okFlag = true;
                                     
                                       QueryStatus status = SaveData(_dataObject);
                                       YMessageBox.Information(status.StatusMessage);

                                       if (status.Status == 0)
                                       {
                                           okFlag = false;
                                       }
                                       if (okFlag)
                                       {
                                           Close();
                                       }
                                   };
                tsOperations.Items.Add(saveC);
            }

            if (UpdateData != null)
            {
                var o = new TData {Id = _Id};
                _dataObject = DetailData(o) as TData;
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

                    var dinCollectAtt = propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                        a => a.GetType() == typeof (DinamicCollectionModelAttribute)) as DinamicCollectionModelAttribute;

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
                                    singleSelectorType.GetProperty("Left").SetValue(c, labelText.Width + labelText.Left,
                                                                                    null);
                                    object txbSv = singleSelectorType.GetField("txbSelectedValue").GetValue(c);
                                    object databindings = txbSv.GetType().GetProperty("DataBindings").GetValue(txbSv,
                                                                                                               null);
                                    databindings.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null,
                                                                        databindings,
                                                                        new object[]
                                                                            {
                                                                                "Text", _dataObject,
                                                                                propertyInfo.Name,
                                                                                false,
                                                                                DataSourceUpdateMode.OnPropertyChanged
                                                                            });
                                    singleSelectorType.GetProperty("SourceProperty").SetValue(c,
                                                                                              singSelectAtt.
                                                                                                  SourceProperty, null);
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
                                    a => a.GetType() == typeof (EditModeAttribute)) as EditModeAttribute;
                            if (propertyInfo.PropertyType == typeof (string))
                            {
                                c = new TextBox
                                        {Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left};
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
                                        {
                                            Height = 200,
                                            Top = _cHeight,
                                            Left = labelText.Width + labelText.Left,
                                            Width = 400
                                        };
                                (c as YDataGridView).DataSource = propertyInfo.GetValue(_dataObject, null);
                                (c as YDataGridView).RowHeadersVisible = false;
                            }

                            if (c != null)
                            {
                                if (
                                    propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                                        a => a.GetType() == typeof (NotEditAttribute)) != null)
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
}