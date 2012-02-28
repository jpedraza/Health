using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using EFCFModel;
using EFCFModel.Attributes;
using EFCFModel.Entities;
using PrototypeHM.Components;
using PrototypeHM.DI;

namespace PrototypeHM.Forms
{
    public partial class EditForm : DIForm
    {
        private readonly DbContext _dbContext;
        private readonly Type _etype;
        private readonly IList<Control> _form;
        private readonly IList<string> _labels;
        private readonly SchemaManager _schemaManager;
        private object _data;
        private int _key;
        private int _top;

        public EditForm(IDIKernel diKernel, Type etype, int key) : base(diKernel)
        {
            InitializeComponent();
            Text = string.Format("Редактирование {0}", etype.GetDisplayName());
            _etype = etype;
            _key = key;
            _dbContext = Get<DbContext>();
            _schemaManager = Get<SchemaManager>();
            _form = new List<Control>();
            _labels = new List<string>();
            InitializeData();
            InitializeForm();
        }

        private void InitializeData()
        {
            if (_schemaManager.HasKey(_etype))
            {
                _data = _key != -1
                            ? ((IQueryable<object>) _dbContext.Set(_etype)).
                                  FirstOrDefault(ExQueryable.WhereProperty(_etype, _schemaManager.Key(_etype).Name, _key))
                            : _dbContext.Set(_etype).Create();
            }
        }

        private void InitializeForm()
        {
            InitializeProperties();
            InitializeRelation();
            InitializeControls();
        }

        private void InitializeProperties()
        {
            _top = 0;
            PropertyInfo[] properties = _etype.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                PropertyInfo prop = property;
                var propertyValue = prop.GetValue(_data, null);
                if (prop.Name == _schemaManager.Key(_etype).Name) continue;
                Type propertyType = prop.PropertyType;
                Control control = null;
                if (propertyType == typeof (int))
                {
                    control = new NumericUpDown { Name = string.Format("nupd{0}", prop.Name) };
                    control.DataBindings.Add("Value", _data, prop.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                if (propertyType == typeof (double))
                {
                    control = new NumericUpDown
                            {
                                Name = string.Format("nupd{0}", prop.Name),
                                DecimalPlaces = 2,
                                Increment = (decimal) 0.5
                            };
                    control.DataBindings.Add("Value", _data, prop.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                if (propertyType == typeof (string))
                {
                    var c = new TextBox
                                  {
                                      Name = string.Format("txb{0}", prop.Name),
                                      Text = prop.GetValue(_data, null) as string
                                  };
                    c.TextChanged += (sender, e) => prop.SetValue(_data, c.Text, null);
                    control = c;
                }
                if (propertyType == typeof (DateTime))
                {
                    var value = propertyValue == null || (DateTime) propertyValue == default(DateTime)
                                    ? DateTime.Now
                                    : (DateTime) propertyValue;
                    var c = new DateTimePicker { Name = string.Format("dtp{0}", prop.Name), Value = value };
                    c.ValueChanged += (sender, e) => prop.SetValue(_data, c.Value, null);
                    prop.SetValue(_data, value, null);
                    control = c;
                }
                if (propertyType == typeof (byte[]))
                {
                    var byteTypeAttribute =
                        prop.GetCustomAttributes(true).FirstOrDefault(att => att is ByteTypeAttribute) as ByteTypeAttribute;
                    if (byteTypeAttribute != null)
                    {
                        var propertyByteValue = (byte[]) propertyValue;
                        Type byteType = byteTypeAttribute.GetByteType(_data);
                        if (byteType == typeof(bool))
                        {
                            bool value = propertyValue != null
                                             ? BitConverter.ToBoolean(propertyByteValue, 0)
                                             : default(bool);
                            var c = new CheckBox { Name = string.Format("chb{0}", prop.Name), Checked = value };
                            c.CheckedChanged +=
                                (sender, e) => prop.SetValue(_data, BitConverter.GetBytes(c.Checked), null);
                            control = c;
                        }
                        if (byteType == typeof (int))
                        {
                            var c = new NumericUpDown { Name = string.Format("nupd{0}", prop.Name) };
                            var value = propertyValue != null
                                            ? BitConverter.ToInt32(propertyByteValue, 0)
                                            : c.Minimum;
                            c.Value = value;
                            c.ValueChanged +=
                                (sender, e) => prop.SetValue(_data, BitConverter.GetBytes(Convert.ToInt32(c.Value)), null);
                            control = c;
                        }
                        if (byteType == typeof (double))
                        {
                            var c = new NumericUpDown
                                    {
                                        Name = string.Format("nupd{0}", prop.Name),
                                        DecimalPlaces = 2,
                                        Increment = (decimal) 0.5
                                    };
                            var value = propertyValue != null
                                            ? (decimal) BitConverter.ToDouble(propertyByteValue, 0)
                                            : c.Minimum;
                            c.Value = value;
                            c.ValueChanged +=
                                (sender, e) => prop.SetValue(_data, BitConverter.GetBytes(Convert.ToDouble(c.Value)), null);
                            control = c;
                        }
                        if (byteType == typeof (string))
                        {
                            string text = propertyValue == null
                                              ? string.Empty
                                              : Encoding.UTF8.GetString((byte[]) prop.GetValue(_data, null));
                            var c = new TextBox
                                    {
                                        Name = string.Format("txb{0}", prop.Name),
                                        Text = text
                                    };
                            c.TextChanged +=
                                (sender, e) => prop.SetValue(_data, Encoding.UTF8.GetBytes(c.Text), null);
                            control = c;
                        }
                        if (byteType == typeof (DateTime))
                        {
                            var value = propertyValue != null
                                            ? DateTime.FromBinary(BitConverter.ToInt64(propertyByteValue, 0))
                                            : DateTime.Now;
                            var c = new DateTimePicker { Name = string.Format("dtp{0}", prop.Name), Value = value };
                            c.ValueChanged +=
                                (sender, e) => prop.SetValue(_data, BitConverter.GetBytes(c.Value.ToBinary()), null);
                            control = c;
                        }
                    }
                }
                if (control != null)
                {
                    control.Top = _top;
                    _top += control.Height;
                    _form.Add(control);
                    var att =
                        prop.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute)
                        as DisplayNameAttribute;
                    _labels.Add(att == null ? prop.Name : att.DisplayName);
                }
            }
        }

        private void InitializeRelation()
        {
            IList<Relation> relations = _schemaManager.GetRelations(_etype);
            foreach (Relation relation in relations)
            {
                Control c = null;
                Relation rel = relation;
                if (relation.RelationType == RelationType.ManyToOne)
                {
                    c = new SingleSelector(DIKernel, relation.FromProperty.PropertyType)
                            {
                                SelectedData = relation.FromProperty.GetValue(_data, null),
                                ValueChange = o => rel.FromProperty.SetValue(_data, o, null)
                            };
                }
                if (relation.RelationType == RelationType.ManyToMany)
                {
                    c = new MultiSelector(DIKernel);
                    object left = relation.FromProperty.GetValue(_data, null);
                    object right = ((IEnumerable<object>) _dbContext.Set(relation.ToType)).
                        Where(e => !((IEnumerable<object>) left).Contains(e)).ToList(relation.ToType);
                    (c as MultiSelector).SetData(left, right);
                }
                if (c != null)
                {
                    _form.Add(c);
                    var att =
                        relation.FromProperty.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute)
                        as DisplayNameAttribute;
                    _labels.Add(att == null ? relation.FromProperty.Name : att.DisplayName);
                }
            }
        }

        private void InitializeControls()
        {
            layoutPanel.ColumnCount = 2;
            layoutPanel.RowCount = _form.Count;
            for (int r = 0; r < layoutPanel.RowCount; r++)
            {
                for (int c = 0; c < layoutPanel.ColumnCount; c += 2)
                {
                    var label = new Label {Text = _labels[r], Top = 5};
                    layoutPanel.Controls.Add(label, c, r);
                    layoutPanel.Controls.Add(_form[r], c + 1, r);
                }
            }
            var but = new ToolStripButton("Сохранить");
            but.Click += ButClick;
            toolPanel.Items.Add(but);
        }

        private void ButClick(object sender, EventArgs e)
        {
            try
            {
                if (_key == -1) _dbContext.Set(_etype).Add(_data);
                _dbContext.SaveChanges();
                if (_key == -1) _key = Convert.ToInt32(_schemaManager.Key(_etype).GetValue(_data, null));
                YMessageBox.Information("Сохранено.");
            }
            catch (Exception exp)
            {
                YMessageBox.Error(exp.Message);
            }
        }
    }
}