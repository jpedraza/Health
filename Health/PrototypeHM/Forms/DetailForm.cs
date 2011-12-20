using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PrototypeHM.Components;
using PrototypeHM.DB.Attributes;
using PrototypeHM.DB.DI;
using PrototypeHM.DB;

namespace PrototypeHM.Forms
{
    public partial class DetailForm : DIForm
    {
        private readonly object _showObject;
        private const int RHeight = 24;
        private int _cHeight;

        public DetailForm()
        {
            InitializeComponent();
        }

        public DetailForm(IDIKernel diKernel, object showObject) : base(diKernel)
        {
            _showObject = showObject;
            InitializeComponent();
            InitializeForms();
        }

        private void InitializeForms()
        {
            InitializeProperties(_showObject);
        }

        private void InitializeProperties(object obj)
        {
            Type objType = obj.GetType();
            PropertyInfo[] propertiesInfo =
                objType.GetProperties().Where(
                    p =>
                    p.GetCustomAttributes(true).Where(
                        a => a.GetType() == typeof (NotDisplayAttribute) || a.GetType() == typeof (HideAttribute)).Count
                        () == 0).ToArray();
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                var propertyValue = propertyInfo.GetValue(obj, null);
                var att =
                    propertyInfo.GetCustomAttributes(true).Where(a => a is DisplayNameAttribute).FirstOrDefault() as
                    DisplayNameAttribute;
                string labelText = att == null ? propertyInfo.Name : att.DisplayName;
                var textLabel = new Label
                                    {
                                        Text = labelText,
                                        Height = RHeight,
                                        Top = _cHeight,
                                        Left = 0
                                    };
                if (propertyValue is ICollection)
                {
                    var dataGridView = new YDataGridView
                                           {
                                               DataSource = propertyValue,
                                               BackgroundColor = Color.White,
                                               AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                                               Top = textLabel.Top + textLabel.Height
                                           };
                    Controls.AddRange(new Control[] { textLabel, dataGridView });
                    _cHeight = dataGridView.Height + dataGridView.Top;
                    Type operationContextType = typeof(OperationsContext<>);
                    Type dObjType = propertyValue.GetType().GetGenericArguments()[0];
                    Type objOperContxType = operationContextType.MakeGenericType(dObjType);
                    var operationsContext =
                        DIKernel.Get<OperationsRepository>().Operations.Where(o => o.GetType() == objOperContxType).
                            FirstOrDefault();
                    if (operationsContext != null)
                    {
                        PropertyInfo pi = operationsContext.GetType().GetProperty(@"Detail");
                        Type methodType = typeof(Func<,>).MakeGenericType(dObjType, typeof(object));
                        var mv = pi.GetValue(operationsContext, null);
                        var pv = propertyValue as ICollection;
                        var arr = new object[pv.Count];
                        pv.CopyTo(arr, 0);
                        if (mv != null)
                        {
                            dataGridView.Detail =
                                clickedRow =>
                                {
                                    object ob = methodType.InvokeMember("DynamicInvoke", BindingFlags.InvokeMethod,
                                                                        null, mv,
                                                                        new[] { arr[clickedRow] });
                                    var form = new DetailForm(DIKernel, ob) { MdiParent = MdiParent };
                                    form.Show();
                                };
                        }
                        dataGridView.InitializeOperations();
                    }
                }
                else
                {
                    string labelValue = propertyValue.ToString();
                    var valueLabel = new Label
                                         {
                                             Text = labelValue,
                                             Top = _cHeight,
                                             Left = textLabel.Width + 10,
                                             AutoSize = true
                                         };
                    _cHeight += RHeight > valueLabel.Height ? RHeight : valueLabel.Height;
                    Controls.AddRange(new[] {textLabel, valueLabel});
                }
            }
        }
    }
}
