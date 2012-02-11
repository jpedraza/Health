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
                DIKernel.Get<OperationsRepository>().Operations.Where(
                    o => o.GetType() == typeof (OperationsContext<TData>)).FirstOrDefault() as
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
                                       /*if (_dgvs.Count == _collectionsPropertiInfos.Count)
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

                                                           if (_dgvs[i].RowCount > 1)
                                                           {
                                                               _collectionsPropertiInfos[i].SetValue(_dataObject, _lclClln, null);
                                                           }
                                                           else
                                                           {
                                                               okFlag = false;
                                                           }

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
                                               */
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
                        if (dinCollectAtt != null)
                        {
                            //c = Activator.CreateInstance(typeof(DinamicCollection)) as Control;
                            var cc = new DinamicCollection();
                            cc.Width = tscContent.ContentPanel.Width - 20;
                            cc.Left = 10;
                            cc.tableWidth = tscContent.ContentPanel.Width - 50;
                            cc.Name = propertyInfo.Name;
                            /*
                             * В случае создания нового элемента, необходимо создать коллекцию.
                             */

                            _dgvs.Add((cc).dgvMetaData);
                            _collectionsPropertiInfos.Add(propertyInfo);
                            /*
                             * Далее необходимо создать столбыцы, которые описаны в сущности                             
                             */
                            PropertyInfo[] elementPropertiesInfo = dinCollectAtt.TypeOfCollectionElement.GetProperties();

                            if (elementPropertiesInfo != null)
                            {
                                foreach (PropertyInfo propertyInfoOfElem in elementPropertiesInfo)
                                {
                                    var elemAttr =
                                        propertyInfoOfElem.GetCustomAttributes(true).FirstOrDefault(
                                            a => a.GetType() == typeof (DisplayNameAttribute)) as DisplayNameAttribute;
                                    if (
                                        propertyInfoOfElem.GetCustomAttributes(true).FirstOrDefault(
                                            a => a.GetType() == typeof (HideAttribute)) == null)
                                    {
                                        var smplAtt = propertyInfoOfElem.GetCustomAttributes(true).FirstOrDefault(
                                            a =>
                                                {
                                                    bool _flag = true;
                                                    if (a.GetType() != typeof (SimpleOrCompoundModelAttribute))
                                                    {
                                                        _flag = false;
                                                    }
                                                    else
                                                    {
                                                        if (a.GetType().GetProperty("IsSimple").GetValue(a, null) ==
                                                            null)
                                                        {
                                                            _flag = false;
                                                        }
                                                    }
                                                    return _flag;
                                                }) as SimpleOrCompoundModelAttribute;

                                        if (smplAtt != null)
                                        {
                                            if (smplAtt.IsSimple)
                                            {
                                                string columnText = !string.IsNullOrEmpty(elemAttr.DisplayName)
                                                                        ? elemAttr.DisplayName
                                                                        : propertyInfoOfElem.Name;
                                                cc.dgvMetaData.Columns.Add(new DataGridViewTextBoxColumn
                                                                               {
                                                                                   Name = propertyInfoOfElem.Name,
                                                                                   HeaderText = columnText,
                                                                                   DataPropertyName =
                                                                                       propertyInfoOfElem.Name
                                                                               });
                                            }
                                            else
                                            {
                                                string columnText = !string.IsNullOrEmpty(elemAttr.DisplayName)
                                                                        ? elemAttr.DisplayName
                                                                        : propertyInfoOfElem.Name;
                                                cc.dgvMetaData.Columns.Add(
                                                    new DataGridViewButtonColumn
                                                        {
                                                            Name = propertyInfoOfElem.Name,
                                                            HeaderText = columnText,
                                                        });
                                                cc.dgvMetaData.CellClick
                                                    += (sender, e) =>
                                                           {
                                                               if (
                                                                   cc.dgvMetaData.Columns[
                                                                       e.ColumnIndex].GetType() ==
                                                                   typeof (DataGridViewButtonColumn
                                                                       ))
                                                               {
                                                                   Type formType = null;
                                                                   PropertyInfo currentProperyInfo
                                                                       = null;
                                                                   foreach (
                                                                       PropertyInfo item1 in
                                                                           propertiesInfo)
                                                                   {
                                                                       if (item1.Name == cc.Name)
                                                                       {
                                                                           formType =
                                                                               typeof (AddForm<>).
                                                                                   MakeGenericType(
                                                                                       item1.
                                                                                           PropertyType
                                                                                           .
                                                                                           GetGenericArguments
                                                                                           ()[0]);
                                                                           currentProperyInfo =
                                                                               item1;
                                                                       }
                                                                   }

                                                                   GetValueFromModalForm(currentProperyInfo, formType);
                                                                   if (UpdateData != null)
                                                                   {
                                                                       //editForm = Activator.CreateInstance(formType, DIKernel, cc.dgvMetaData["Id", e.RowIndex]);
                                                                   }
                                                               }
                                                           };
                                            }
                                        }
                                    }
                                }
                            }

                            cc.AddButtonClick = (sender, e) =>
                                                    {
                                                        Debug.Assert(cc != null, "cc != null");
                                                        cc.dgvMetaData.Rows.Add();
                                                    };

                            cc.DeleteButtonClick = (sender, e) =>
                                                       {
                                                           int delRowIndex = 0;
                                                           for (int i = 0; i < cc.dgvMetaData.RowCount; i++)
                                                           {
                                                               for (int j = 0; j < cc.dgvMetaData.ColumnCount; j++)
                                                               {
                                                                   if (cc.dgvMetaData.Rows[i].Cells[j].Selected)
                                                                   {
                                                                       delRowIndex = i;
                                                                       i = cc.dgvMetaData.RowCount;
                                                                       break;
                                                                   }
                                                               }
                                                           }
                                                           if (cc.dgvMetaData.RowCount > 1 &&
                                                               delRowIndex != cc.dgvMetaData.RowCount - 1)
                                                           {
                                                               cc.dgvMetaData.Rows.RemoveAt(delRowIndex);
                                                           }
                                                       };
                            c = cc;
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

        private void GetValueFromModalForm(PropertyInfo currentProperyInfo, Type formType)
        {
            if (SaveData != null && formType != null)
            {
                object editForm = Activator.CreateInstance(formType,new object[]{DIKernel, -1});
                formType.GetMethod("InitializeForm").Invoke(editForm, null);
                var form = editForm as Form;
                if (form != null)
                {
                    form.ShowDialog();
                    form.FormClosing +=
                        (sender2, e2) =>
                            {
                                editForm = form;
                                var testObject = formType.GetProperty(@"_dataObject").GetValue(this, null);
                                if(testObject != null)
                                {
                                    if (testObject.GetType().GetMethod("IsValid") != null)
                                    {
                                        var resultTestValid = testObject.GetType().GetMethod("IsValid").Invoke(
                                            editForm, null);
                                        if (resultTestValid != null)
                                        {
                                            if (resultTestValid.ToString() == "true")
                                            {
                                                e2.Cancel = false;
                                            }
                                            if (resultTestValid.ToString()=="false")
                                            {
                                                var dialogResultAboutFormClosing =
                                                    YMessageBox.Dialog(
                                                        "Внимание, созданый экземпляр не прошел проверку на ваидацию. Вы действительно хотите закрыть форму?");
                                                if (dialogResultAboutFormClosing == DialogResult.Yes)
                                                {
                                                    e2.Cancel = false;
                                                } 
                                                if (dialogResultAboutFormClosing == DialogResult.No | dialogResultAboutFormClosing == DialogResult.Cancel)
                                                {
                                                    e2.Cancel = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            };
                    //Запись объекта из модальной формы
                    object currentPropertyValue = formType.GetField(
                                    "_dataObject").GetValue(editForm);

                    if (currentPropertyValue != null)
                    {
                        
                        if (currentProperyInfo.GetValue(_dataObject,null) ==null)
                        {
                            //Если текущий объект не создан, то создаем его

                            if (currentProperyInfo.PropertyType.Name ==
                                typeof (ICollection<>).Name)
                            {
                                currentProperyInfo.SetValue(_dataObject, Activator.CreateInstance(
                                    typeof (Collection<>).MakeGenericType(
                                        currentProperyInfo.PropertyType.GetGenericArguments()[0])), null);
                            }

                            if (currentProperyInfo.PropertyType.Name ==
                                typeof (IList<>).Name)
                            {
                                currentProperyInfo.SetValue(_dataObject,
                                                            Activator.CreateInstance(typeof (List<>).MakeGenericType(
                                                                currentProperyInfo.PropertyType.GetGenericArguments()[0])),
                                                            null);
                            }

                            if (currentProperyInfo.PropertyType.Name !=
                            typeof(ICollection<>).Name & currentProperyInfo.PropertyType.Name !=
                            typeof(IList<>).Name)
                            {
                                currentProperyInfo.SetValue(_dataObject,
                                                            Activator.CreateInstance(currentProperyInfo.PropertyType),
                                                            null);
                            }
                            //конец создания
                        }

                        if (currentProperyInfo.GetValue(_dataObject, null) != null)
                        {
                            //далее если объект создался или уже ьыл создан, то
                            if (currentProperyInfo.PropertyType.Name !=
                            typeof(ICollection<>).Name & currentProperyInfo.PropertyType.Name !=typeof(IList<>).Name)
                            {
                                //записываем в объект значение
                                 currentProperyInfo.SetValue(
                                _dataObject, currentPropertyValue, null);
                            }
                            else
                            {
                                //либо добавляем значение в коллекцию
                                
                                //Вытаскиваем метод вставки
                                var methodAdd = currentProperyInfo.PropertyType.GetMethod("Insert");

                                //Далее вычисляем будущий индекс элемента:
                                var countOfCollecction =
                                    currentProperyInfo.PropertyType.GetProperty("Count").GetValue(_dataObject, null) as
                                    int?;

                                //Если не удалось вытащить индекс, то присваиваем 1.
                                var futureIndex = countOfCollecction != null ? countOfCollecction + 1 : 1;
                                //конец вычисления индекса.


                                methodAdd.Invoke(_dataObject, new[]
                                        {
                                            futureIndex,
                                            currentPropertyValue
                                        });
                            }
                            //конец записи в объект.
                        }
                        
                    }
                    //конец записи из модальной формы
                }
            }
        }
    }
}