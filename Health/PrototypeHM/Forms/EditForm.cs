using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PrototypeHM.Components;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;
using PrototypeHM.DB.DI;
using PrototypeHM.Properties;
using PrototypeHM;

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
                    if (operationsContext.Save == null)
                    {
                        throw new Exception(string.Format("Отсутствует операция сохранения, для типа: {0}",
                                                          typeof (TData).Name));
                    }
                    SaveData = operationsContext.Save;
                    _dataObject = new TData();
                }
                else
                {
                    if (operationsContext.Update == null)
                    {
                        throw new Exception(string.Format("Отсутствует операция обновления, для типа: {0}",
                                                          typeof (TData).Name));
                    }
                    UpdateData = operationsContext.Update;
                    DetailData = operationsContext.Detail;
                }
            }

            else
            {
                throw new Exception(string.Format("Отсутствует контекст операций, для типа: {0}", typeof(TData).Name));
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

                if (_dataObject == null)
                {
                    throw new Exception("Отсутствует объект редактирования");
                }
            }


            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                //Если null, то сперва необходимо создать.
                if (propertyInfo.GetValue(_dataObject,null) == null)
                {
                    propertyInfo.SetValue(_dataObject, Activator.CreateInstance(propertyInfo.PropertyType, null), null);
                }
                
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
                        //Обработка свойств помеченных атрибутом MultiSelector
                        var multiSelectEdit =
                            propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                                o => o.GetType() == typeof (MultiSelectEditModeAttribute)) as
                            MultiSelectEditModeAttribute;

                        if (multiSelectEdit != null)
                        {
                            //Определяем тип связи, который имеет данное свойство с отрисовываемой сущностью:
                            var typeMappingEnum = multiSelectEdit.Type;
                            if (typeMappingEnum == 0)
                            {
                                //Если не указан тип связи, выводим соответствующее значение:
                                c = new Label
                                        {
                                            Height = LHeight*3,
                                            Top = _cHeight,
                                            Left = labelText.Width,
                                            ForeColor = Color.Red,
                                            Width = Convert.ToInt32(this.Width-0.1*this.Width),
                                            Text =
                                                String.Format(
                                                    "Для типа данных {0} \n необходимо указать тип связей между сущностями для свойства: \n {1}",
                                                    typeof (TData).Name, propertyInfo.Name)
                                        };
                            }
                            else
                            {
                                #region SaveData 
                                //Если тип связи указан, то определяем в каком режиме работает данная форма:
                                if (SaveData != null)
                                {
                                    //В случае сохранения данных:

                                    //Определяем тип связи:
                                    if (typeMappingEnum == TypeMappingEnum.ManyToOne)
                                    {
                                        //Если на момент создания объекта данное свойство не заполнить:
                                        c = new Label
                                        {
                                            Height = LHeight * 3,
                                            Top = _cHeight,
                                            Left = labelText.Width + labelText.Left,
                                            ForeColor = Color.Salmon,
                                            Width = Convert.ToInt32(this.Width - 0.1 * this.Width),
                                            Text =
                                                String.Format(
                                                    "Для заполнения свойства {0} \n необходимо сперва создать сам объект", displayNameAttribute.DisplayName)
                                        };
                                    }
                                    else
                                    {
                                        //Если заполнение возможно, то добавляем на форму MultiSelector
                                        if (multiSelectEdit!=null && multiSelectEdit.OperationContext!=null && multiSelectEdit.SourcePropery!=null)
                                        {
                                            //Определяем контекст операций для данных добавляемых в MultiSelector
                                            var operationContext2 = Get<OperationsRepository>().Operations.FirstOrDefault(o => o.GetType() == multiSelectEdit.OperationContext);
                                            if (operationContext2 != null)
                                            {
                                                
                                                //Получаем делегат, представляющий метод загрузки данных
                                                var @delegate =
                                                    operationContext2.GetType().GetProperty("Load").GetValue(operationContext2, null);

                                                //Если такая операция не предумотрена, то кидаем исключение:
                                                if (@delegate == null)
                                                {
                                                    throw new Exception(
                                                        string.Format("Отсутствует операция загрузки для типа \n {0}",
                                                                      multiSelectEdit.OperationContext.FullName));
                                                }

                                                //Вызываем через делегат метод загрузки данных
                                                var leftListData = (@delegate as Delegate).DynamicInvoke(null);

                                                //Добавляем MultiSelector
                                                c = new MultiSelector() { Top = _cHeight, Left = labelText.Width + labelText.Left };

                                                //Инициализируем загрузку данных
                                                var toBindingMethod =
                                                    typeof (ExtensionMethods).GetMethod("ToBindingList",
                                                                                        BindingFlags.Public |
                                                                                        BindingFlags.Static);

                                                if (toBindingMethod == null)
                                                    throw new Exception(
                                                        string.Format(
                                                            "Ошибка, тип {0}\n не поддерживает метод ToBindingList()",
                                                            leftListData.GetType().FullName));

                                                var toBindingMethodGenericArgs = toBindingMethod.GetGenericMethodDefinition();

                                                //Создаем метод с генерик-аргументом
                                                var newToBindingMethod =
                                                    toBindingMethodGenericArgs.MakeGenericMethod(
                                                        propertyInfo.PropertyType.GetGenericArguments()[0]);

                                                //Подгружаем даные
                                                typeof (MultiSelector).GetMethod("SetData").Invoke(
                                                    (c as MultiSelector),
                                                    new[]
                                                        {
                                                            newToBindingMethod.Invoke(null, BindingFlags.Static, null,
                                                                                      new[] {leftListData}, null),
                                                            newToBindingMethod.Invoke(null, BindingFlags.Static, null,
                                                                                      new[]
                                                                                          {
                                                                                              propertyInfo.GetValue(
                                                                                                  _dataObject, null)
                                                                                          },
                                                                                      null)
                                                                                      //этого хватит?
                                                        });





                                            }
                                            else
                                            {
                                                throw new Exception(
                                                    string.Format("Отсутствует контекст операций для типа {0}",
                                                                  propertyInfo.PropertyType.FullName));
                                            }
                                        }
                                        else
                                        {
                                            //Иначе выводим сообщение об ошибке:
                                            c = new Label
                                            {
                                                Height = LHeight * 3,
                                                Top = _cHeight,
                                                Left = labelText.Width + labelText.Left,
                                                ForeColor = Color.Red,
                                                Width = Convert.ToInt32(this.Width - 0.1 * this.Width),
                                                Text =
                                                    String.Format(
                                                        "Ошибка при навешивании атрибутов на св-во {0} \n в типе: \n {1}", propertyInfo.Name, typeof(TData).Name)
                                            };
                                        }

                                    }
                                    //Конец отрисовки для сохранения данных
                                #endregion
                                }
                                if(UpdateData != null)
                                {
                                    //В случае редактирования (обновления) данных

                                    //Конец отрисовки редактирования данных
                                }
                            }

                        }
                        //Конец обработки свойств помеченных атрибутом MultiSelector

                        else
                        {
                            
                            //Обработка прочих свойств
                            
                            var editModeAtt =
                                propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                                    a => a.GetType() == typeof (EditModeAttribute)) as EditModeAttribute;
                            if (propertyInfo.PropertyType == typeof (string))
                            {
                                c = new TextBox
                                        {Height = LHeight, Top = _cHeight, Left = labelText.Width + labelText.Left, Width = 200};
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

                            
                            //конец обработки прочих свойств
                        }

                        
                    }

                    //Добавляем на форму готовый элемент
                    if (c != null)
                    {
                        if (
                            propertyInfo.GetCustomAttributes(true).FirstOrDefault(
                                a => a.GetType() == typeof(NotEditAttribute)) != null)
                        {
                            c.Enabled = false;
                        }
                        _cHeight += c.Height + 5;
                        tscContent.ContentPanel.Controls.AddRange(new[] { c, labelText });
                    }
                    //конец добавления элемента
                }
            }
        }
       
    }
}