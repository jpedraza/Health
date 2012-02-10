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
using PrototypeHM.Parameter;
using System.Collections.ObjectModel;
using System.Collections;

namespace PrototypeHM.Forms
{
    public partial class AddForm<TData> : DIForm
        where TData : class, IIdentity,  new()
    {
        private TData _dataObject;

        public Func<TData, QueryStatus> SaveData { get; set; }

        public Func<TData, object> DetailData { get; set; }

        public Func<TData, QueryStatus> UpdateData { get; set; }

        private int _cHeight = 15;
        private const int LHeight = 24;

        private int _Id;

        public AddForm()
        {
            BaseConstructor();
        }

        public AddForm(IDIKernel diKernel)
            : base(diKernel)
        {
            BaseConstructor();
        }

        public AddForm(IDIKernel diKernel, int Id) : base(diKernel) {
            _Id = Id;
            BaseConstructor();
        }

        private void BaseConstructor()
        {
            
            InitializeComponent();
        }

        /*
         * Создаем служебный список, хранящий все таблицы, хранящие коллекции 
         * отображаемого типа данных
         */

        private IList<YDataGridView> _dgvs;

        /*
         Создаем служебный список, хранящий всю информацию о типах данных,
         отображаемых коллекций (дженериков)
         */

        private IList<PropertyInfo> _collectionsPropertiInfos;

        public void InitializeForm()
        {
            _dgvs = new List<YDataGridView>();
            _collectionsPropertiInfos = new List<PropertyInfo>();
            
            Type dataType = typeof(TData);
            PropertyInfo[] propertiesInfo = dataType.GetProperties();

            var operationsContext =
                DIKernel.Get<OperationsRepository>().Operations.Where(
                    o => o.GetType() == typeof(OperationsContext<TData>)).FirstOrDefault() as
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
                var saveC = new ToolStripButton { DisplayStyle = ToolStripItemDisplayStyle.Text, Text = @"Сохранить" };
                saveC.Click += (sender, e) =>
                                           {
                                               
                                               //Флаг, показывающий - нормально ли сохранились данные
                                               var okFlag = true;
                                               if (_dgvs.Count == _collectionsPropertiInfos.Count)
                                               {
                                                   for (var i = 0; i < _dgvs.Count; i++)
                                                   {
                                                       if (_dgvs[i] != null)
                                                       {
                                                           var _att = _collectionsPropertiInfos[i].GetCustomAttributes(true).
                                                                       FirstOrDefault(a => a.GetType() == typeof(DinamicCollectionModelAttribute)) as DinamicCollectionModelAttribute;

                                                           var _tp = _att.TypeOfCollectionElement;

                                                           var _tpc = typeof(Collection<>);

                                                           var _cstrTpc = _tpc.MakeGenericType(_tp);

                                                           var _lclClln = Activator.CreateInstance(_cstrTpc);

                                                           var _pis = _cstrTpc.GetProperties();

                                                           var _linkForTable = _dgvs[i];

                                                           for (var _rowIndex = 0; _rowIndex < _dgvs[i].RowCount - 1; _rowIndex++)
                                                               //Последнюю строчку не учитываем, в связи с тем, что она 
                                                               //автоматически подстанавливается
                                                           {
                                                               try
                                                               {
                                                                   var _localRow = Activator.CreateInstance(_tp);

                                                                   for (var _columnIndex = 0; _columnIndex < _dgvs[i].ColumnCount; _columnIndex++)
                                                                   {
                                                                       try
                                                                       {
                                                                           
                                                                           _tp.GetProperty(_dgvs[i].Columns[_columnIndex].Name)
                                                                               .SetValue(_localRow, _linkForTable[_columnIndex, _rowIndex].Value, null);
                                                                       }
                                                                       catch { }
                                                                   }
                                                                   _cstrTpc.GetMethod("Add").Invoke(_lclClln, new object[] { _localRow });
                                                               }
                                                               catch
                                                               {

                                                               }
                                                               
                                                           }

                                                           _collectionsPropertiInfos[i].SetValue(_dataObject, _lclClln, null);

                                                       }
                                                       else
                                                       {
                                                           okFlag = false;
                                                           break;
                                                       }
                                                   }
                                               }
                                               else
                                               { okFlag = false; }

                                               QueryStatus status = SaveData(_dataObject);
                                               YMessageBox.Information(status.StatusMessage);

                                               if (status.Status == 1)
                                                   okFlag = false;
                                                   if (okFlag)
                                                       this.Close();
                                           };
                tsOperations.Items.Add(saveC);
            }

            if (UpdateData != null)
            {
                TData o = new TData { Id = _Id };
                _dataObject = DetailData(o) as TData;

            }

            //служебная переменная
            PropertyInfo propertyInfoSpecial;
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                if (propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                        a => a.GetType() == typeof(NotDisplayAttribute) || a.GetType() == typeof(HideAttribute)) == null)
                {
                    string textForLabel = propertyInfo.Name;
                    var displayNameAttribute =
                        propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                            a => a.GetType() == typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                    if (displayNameAttribute != null) textForLabel = displayNameAttribute.DisplayName;
                    var labelText = new Label { Text = textForLabel, Height = LHeight, Top = _cHeight, Left = 15 };
                    Control c = null;
                    var singSelectAtt =
                        propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                            a => a.GetType() == typeof(SingleSelectEditModeAttribute)) as SingleSelectEditModeAttribute;

                    var dinCollectAtt = propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                        a => a.GetType() == typeof(DinamicCollectionModelAttribute)) as DinamicCollectionModelAttribute;

                    if (singSelectAtt != null && singSelectAtt.OperationContext != null)
                    {
                        object operationContext = Get<OperationsRepository>().Operations.FirstOrDefault(
                            o => o.GetType() == singSelectAtt.OperationContext);
                        if (operationContext != null)
                        {
                            Type funcType =
                                typeof(Func<>).MakeGenericType(
                                    typeof(IList<>).MakeGenericType(
                                        singSelectAtt.OperationContext.GetGenericArguments()[0]));
                            if (operationContext.GetType().GetProperty("Load").PropertyType == funcType)
                            {
                                object loadProperty =
                                    operationContext.GetType().GetProperty("Load").GetValue(operationContext, null);
                                if (loadProperty != null)
                                {
                                    Type singleSelectorType =
                                        typeof(SingleSelector<>).MakeGenericType(
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
                        if (dinCollectAtt != null) {
                            //c = Activator.CreateInstance(typeof(DinamicCollection)) as Control;
                            var cc = new DinamicCollection();
                            cc.Width = tscContent.ContentPanel.Width - 20;
                            cc.Left = 10;
                            cc.tableWidth = tscContent.ContentPanel.Width - 50;
                            /*
                             * В случае создания нового элемента, необходимо создать коллекцию.
                             */
                            
                            _dgvs.Add((cc as DinamicCollection).dgvMetaData);
                            _collectionsPropertiInfos.Add(propertyInfo);
                            /*
                             * Далее необходимо создать столбыцы, которые описаны в сущности                             
                             */
                            PropertyInfo[] elementPropertiesInfo = dinCollectAtt.TypeOfCollectionElement.GetProperties();

                            if (elementPropertiesInfo != null)
                            {
                                var fieldNumeric = cc.dgvMetaData.ColumnCount;
                                foreach (PropertyInfo propertyInfoOfElem in elementPropertiesInfo)
                                {
                                    var elemAttr = propertyInfoOfElem.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                                    if (propertyInfoOfElem.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(HideAttribute)) == null)
                                    {
                                        var columnText = elemAttr.DisplayName != null && elemAttr.DisplayName != "" ? elemAttr.DisplayName : propertyInfoOfElem.Name;
                                        cc.dgvMetaData.Columns.Add(new DataGridViewTextBoxColumn() { Name = propertyInfoOfElem.Name, HeaderText = columnText, DataPropertyName = propertyInfoOfElem.Name });
                                        fieldNumeric++;
                                    }                                    
                                }
                                var fieldWidth = 0;
                                if (fieldNumeric != 0)
                                {
                                    fieldWidth = cc.dgvMetaData.Width / fieldNumeric;
                                    fieldWidth = fieldWidth > 15 ? fieldWidth : 15;
                                }                                
                            }

                            cc.AddButtonClick = (sender, e) => {
                                cc.dgvMetaData.Rows.Add();
                            };

                            cc.DeleteButtonClick = (sender, e) => {
                                var delRowIndex = 0;
                                for (var i = 0; i < cc.dgvMetaData.RowCount; i++) {
                                    for (var j = 0; j < cc.dgvMetaData.ColumnCount; j++)
                                    {
                                        if (cc.dgvMetaData.Rows[i].Cells[j].Selected)
                                        {
                                            delRowIndex = i;
                                            i = cc.dgvMetaData.RowCount;
                                            break;
                                        }
                                    }
                                }
                                if (cc.dgvMetaData.RowCount > 1 && delRowIndex != cc.dgvMetaData.RowCount -1)
                                {
                                    cc.dgvMetaData.Rows.RemoveAt(delRowIndex);
                                }
                            };
                            c = cc as Control;
                            if (c != null)
                            {
                                if (propertyInfo.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(NotEditAttribute)) != null)
                                {
                                    c.Enabled = false;
                                }
                                _cHeight += c.Height + 5;
                                tscContent.ContentPanel.Controls.AddRange(new[] { c, labelText });
                            }
                        }
                        else {
                            var editModeAtt =
                                propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                                    a => a.GetType() == typeof(EditModeAttribute)) as EditModeAttribute;
                            if (propertyInfo.PropertyType == typeof(string))
                            {
                                c = new TextBox { Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left };
                                c.DataBindings.Add("Text", _dataObject, propertyInfo.Name, false,
                                                   DataSourceUpdateMode.OnPropertyChanged);
                                if (editModeAtt != null && editModeAtt.Mode.HasFlag(EditModeEnum.Multiline))
                                {
                                    (c as TextBox).Multiline = true;
                                    (c as TextBox).ScrollBars = ScrollBars.Both;
                                    c.Height = c.Height * 3;
                                    c.Width = c.Width * 3;
                                }
                            }
                            if (propertyInfo.PropertyType == typeof(DateTime))
                            {
                                c = new DateTimePicker { Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left };
                                c.DataBindings.Add("Value", _dataObject, propertyInfo.Name, false,
                                                   DataSourceUpdateMode.OnPropertyChanged);
                            }
                            if (propertyInfo.PropertyType == typeof(int))
                            {
                                c = new NumericUpDown { Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left };
                                c.DataBindings.Add("Value", _dataObject, propertyInfo.Name, false,
                                                   DataSourceUpdateMode.OnPropertyChanged);
                            }
                            if (propertyInfo.PropertyType.Name == typeof(ICollection<>).Name)
                            {
                                c = new YDataGridView { Height = 200, Top = _cHeight, Left = labelText.Width + labelText.Left, Width = 400 };
                                (c as YDataGridView).DataSource = propertyInfo.GetValue(_dataObject, null);
                                (c as YDataGridView).RowHeadersVisible = false;
                            }
                        
                            if (c != null)
                            {
                                if (propertyInfo.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(NotEditAttribute)) != null)
                                {
                                    c.Enabled = false;
                                }
                                _cHeight += c.Height + 5;
                                tscContent.ContentPanel.Controls.AddRange(new[] { c, labelText });
                            }
                        }
                    }

                    
                }
            }
        }
    }
}
