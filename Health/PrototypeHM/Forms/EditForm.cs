using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EFCFModel;
using EFCFModel.Attributes;
using PrototypeHM.Components;
using PrototypeHM.DI;
using ByteConverter = EFCFModel.ByteConverter;
using ValidationResult = EFCFModel.ValidationResult;
using Validator = EFCFModel.Validator;

namespace PrototypeHM.Forms
{
    public partial class EditForm : DIForm
    {
        private readonly DbContext _dbContext;
        private readonly Type _etype;
        private readonly IList<Control> _form;
        private readonly IList<string> _labels;
        private readonly SchemaManager _schemaManager;
        private readonly Validator _validator;
        private object _data;
        private int _key;
        private int _top;

        public EditForm(IDIKernel diKernel, Type etype, int key) : base(diKernel)
        {
            UID = etype.FullName + key;
            InitializeComponent();
            Text = string.Format("Редактирование {0}", etype.GetDisplayName());
            _etype = etype;
            _key = key;
            _dbContext = Get<DbContext>();
            _schemaManager = Get<SchemaManager>();
            _validator = new Validator();
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
                                  FirstOrDefault(ExQueryable.PropertyFilter(_etype, _schemaManager.Key(_etype).Name, _key))
                            : _dbContext.Set(_etype).Create();
            }
        }

        private void InitializeForm()
        {
            InitializeProperties();
            InitializeRelation();
            InitializeControls();
        }

        private string ComponentNameForProperty(string name)
        {
            return string.Format("componentFor{0}", name);
        }

        private Control ControlForProperty(string name)
        {
            return layoutPanel.Controls[ComponentNameForProperty(name)];
        }

        private void ValidateProperty(PropertyDescriptor descriptor)
        {
            ValidationResult result = _validator.ValidateProperty(_data, descriptor);
            if (result != null)
                errorProvider.SetError(ControlForProperty(result.Descriptor.Name), result.ErrorMessage);
        }

        private bool ValidateObject()
        {
            bool isValid = _validator.IsValid(_data);
            if (!isValid)
            {
                foreach (ValidationResult error in _validator.Errors)
                    errorProvider.SetError(ControlForProperty(error.Descriptor.Name), error.ErrorMessage);
                statusPanel.Text = @"Исправте ошибки.";
            }
            return isValid;
        }

        private void InitializeProperties()
        {
            //TODO: с этим надо что-то делать.
            _top = 0;
            IEnumerable<PropertyDescriptor> properties = TypeDescriptor.GetProperties(_etype).Cast<PropertyDescriptor>();
            foreach (PropertyDescriptor property in properties)
            {
                PropertyDescriptor prop = property;
                var propertyValue = prop.GetValue(_data);
                if (prop.Name == _schemaManager.Key(_etype).Name) continue;
                Type propertyType = prop.PropertyType;
                Control control = null;
                if (propertyType == typeof (int))
                {
                    var c = new NumericUpDown { Name = ComponentNameForProperty(prop.Name) };
                    c.DataBindings.Add("Value", _data, prop.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                    c.ValueChanged += (sender, e) => ValidateProperty(prop);
                    control = c;
                }
                if (propertyType == typeof (double))
                {
                    var c = new NumericUpDown
                            {
                                Name = ComponentNameForProperty(prop.Name),
                                DecimalPlaces = 2,
                                Increment = (decimal) 0.5
                            };
                    c.DataBindings.Add("Value", _data, prop.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                    c.ValueChanged += (sender, e) => ValidateProperty(prop);
                    control = c;
                }
                if (propertyType == typeof (string))
                {
                    var c = new TextBox
                                  {
                                      Name = ComponentNameForProperty(prop.Name),
                                      Text = prop.GetValue(_data) as string
                                  };
                    var att = prop.Attributes.OfType<EditModeAttribute>().FirstOrDefault();
                    if (att != null && att.GetEditMode() == EditMode.Multiline)
                    {
                        c.Multiline = true;
                        c.Height = c.Height*4;
                        c.Width = c.Width*3;
                        c.ScrollBars = ScrollBars.Vertical;
                    }
                    c.TextChanged += (sender, e) => prop.SetValue(_data, c.Text);
                    c.TextChanged += (sender, e) => ValidateProperty(prop);
                    control = c;
                }
                if (propertyType == typeof (DateTime))
                {
                    var value = propertyValue == null || (DateTime) propertyValue == default(DateTime)
                                    ? DateTime.Now
                                    : (DateTime) propertyValue;
                    var c = new DateTimePicker { Name = ComponentNameForProperty(prop.Name), Value = value };
                    c.ValueChanged += (sender, e) => prop.SetValue(_data, c.Value);
                    c.ValueChanged += (sender, e) => ValidateProperty(prop);
                    prop.SetValue(_data, value);
                    control = c;
                }
                if (propertyType == typeof (byte[]))
                {
                    var byteTypeAttribute = prop.Attributes.OfType<ByteTypeAttribute>().FirstOrDefault();
                    if (byteTypeAttribute != null)
                    {
                        var propertyByteValue = (byte[]) propertyValue;
                        Type byteType = byteTypeAttribute.GetByteType(_data);
                        if (byteType == typeof(bool))
                        {
                            bool value = propertyValue != null
                                             ? Get<ByteConverter>().To<bool>(propertyByteValue)
                                             : default(bool);
                            var c = new CheckBox { Name = ComponentNameForProperty(prop.Name), Checked = value };
                            c.CheckedChanged += (sender, e) => prop.SetValue(_data, Get<ByteConverter>().Get(c.Checked));
                            c.CheckedChanged+= (sender, e) => ValidateProperty(prop);
                            control = c;
                        }
                        if (byteType == typeof (int))
                        {
                            var c = new NumericUpDown { Name = ComponentNameForProperty(prop.Name) };
                            var value = propertyValue != null
                                            ? Get<ByteConverter>().To<int>(propertyByteValue)
                                            : c.Minimum;
                            c.Value = value;
                            c.ValueChanged += (sender, e) => prop.SetValue(_data, Get<ByteConverter>().Get(Convert.ToInt32(c.Value)));
                            c.ValueChanged += (sender, e) => ValidateProperty(prop);
                            control = c;
                        }
                        if (byteType == typeof (double))
                        {
                            var c = new NumericUpDown
                                    {
                                        Name = ComponentNameForProperty(prop.Name),
                                        DecimalPlaces = 2,
                                        Increment = (decimal) 0.5
                                    };
                            var value = propertyValue != null
                                            ? (decimal) Get<ByteConverter>().To<double>(propertyByteValue)
                                            : c.Minimum;
                            c.Value = value;
                            c.ValueChanged += (sender, e) => prop.SetValue(_data, Get<ByteConverter>().Get(Convert.ToDouble(c.Value)));
                            c.ValueChanged += (sender, e) => ValidateProperty(prop);
                            control = c;
                        }
                        if (byteType == typeof (string))
                        {
                            string text = propertyValue == null
                                              ? string.Empty
                                              : Get<ByteConverter>().To<string>(propertyByteValue);
                            var c = new TextBox
                                    {
                                        Name = ComponentNameForProperty(prop.Name),
                                        Text = text
                                    };
                            c.TextChanged += (sender, e) => prop.SetValue(_data, Get<ByteConverter>().Get(c.Text));
                            c.TextChanged += (sender, e) => ValidateProperty(prop);
                            control = c;
                        }
                        if (byteType == typeof (DateTime))
                        {
                            var value = propertyValue != null
                                            ? Get<ByteConverter>().To<DateTime>(propertyByteValue)
                                            : DateTime.Now;
                            var c = new DateTimePicker { Name = ComponentNameForProperty(prop.Name), Value = value };
                            c.ValueChanged += (sender, e) => prop.SetValue(_data, Get<ByteConverter>().Get(c.Value));
                            c.ValueChanged += (sender, e) => ValidateProperty(prop);
                            control = c;
                        }
                    }
                }
                if (control != null)
                {
                    control.Leave += (sender, e) => ValidateProperty(prop);
                    control.Top = _top;
                    _top += control.Height;
                    _form.Add(control);
                    var att = prop.Attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
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
                if (rel.RelationType == RelationType.ManyToOne)
                {
                    c = new SingleSelector(DIKernel, rel.FromProperty.PropertyType)
                            {
                                SelectedData = rel.FromProperty.GetValue(_data, null),
                                ValueChange = o => rel.FromProperty.SetValue(_data, o, null)
                            };
                }
                if (rel.RelationType == RelationType.ManyToMany)
                {
                    c = new MultiSelector(DIKernel);
                    object left = rel.FromProperty.GetValue(_data, null);
                    //TODO: переписать через IQueryable.
                    object right = ((IEnumerable<object>) _dbContext.Set(rel.ToType)).
                        Where(e => !((IEnumerable<object>)left).Contains(e)).IterateToList(relation.ToType);
                    (c as MultiSelector).SetData(left, right);
                }
                if (c != null)
                {
                    c.Name = ComponentNameForProperty(relation.FromProperty.Name);
                    _form.Add(c);
                    var att =
                        rel.FromProperty.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute)
                        as DisplayNameAttribute;
                    _labels.Add(att == null ? rel.FromProperty.Name : att.DisplayName);
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
            var saveButton = new ToolStripButton("Сохранить");
            saveButton.Click += SaveButtonClick;
            toolPanel.Items.Add(saveButton);
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (ValidateObject())
                {
                    if (_key == -1) _dbContext.Set(_etype).Add(_data);
                    _dbContext.SaveChanges();
                    if (_key == -1)
                    {
                        _key = Convert.ToInt32(_schemaManager.Key(_etype).GetValue(_data, null));
                        UID = _etype.FullName + _key;
                    }
                    statusPanel.Text = string.Format("Сохранено {0}.", DateTime.Now);
                }  
            }
            catch (Exception exp)
            {
                YMessageBox.Error(exp.Message);
            }
        }
    }
}