using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EFCFModel;
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
                //TODO: переписать через IQueryable.
                Func<object, bool> where =
                    o => Convert.ToInt32(_schemaManager.Key(_etype).GetValue(o, null)) == _key;
                _data = _key != -1
                            ? _dbContext.Set(_etype).FirstOrDefault(_etype, where)
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
                if (property.Name == _schemaManager.Key(_etype).Name) continue;
                Type propertyType = property.PropertyType;
                Control c = null;
                if (propertyType == typeof (int))
                {
                    c = new NumericUpDown {Name = string.Format("nupd{0}", property.Name)};
                    c.DataBindings.Add("Value", _data, property.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                if (propertyType == typeof (double))
                {
                    c = new NumericUpDown
                            {
                                Name = string.Format("nupd{0}", property.Name),
                                DecimalPlaces = 2,
                                Increment = (decimal) 0.5
                            };
                    c.DataBindings.Add("Value", _data, property.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                if (propertyType == typeof (string))
                {
                    c = new TextBox {Name = string.Format("txb{0}", property.Name)};
                    c.DataBindings.Add("Text", _data, property.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                if (propertyType == typeof (DateTime))
                {
                    c = new DateTimePicker
                            {Name = string.Format("dtp{0}", property.Name)};
                    c.DataBindings.Add("Value", _data, property.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                }
                if (c != null)
                {
                    c.Top = _top;
                    _top += c.Height;
                    _form.Add(c);
                    var att =
                        property.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute)
                        as DisplayNameAttribute;
                    _labels.Add(att == null ? property.Name : att.DisplayName);
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