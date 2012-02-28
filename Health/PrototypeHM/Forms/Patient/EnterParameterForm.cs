using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFCFModel.Entities;
using PrototypeHM.DI;

namespace PrototypeHM.Forms.Patient
{
    public partial class EnterParameterForm : DIForm
    {
        private readonly int _id;
        private readonly DbContext _dbContext;
        private ParameterStorage[] _storages;
        private readonly IList<Control> _form;
        private readonly IList<string> _labels; 

        public EnterParameterForm(IDIKernel diKernel, int id) : base(diKernel)
        {
            _id = id;
            _form = new BindingList<Control>();
            _labels = new BindingList<string>();
            _dbContext = Get<DbContext>();
            InitializeComponent();
            LoadParameters();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            var button = new ToolStripButton("Save");
            button.Click += SaveClick;
            toolPanel.Items.Add(button);
        }

        private void SaveClick(object sender, EventArgs e)
        {
            try
            {
                foreach (ParameterStorage parameterStorage in _storages)
                    _dbContext.Set<ParameterStorage>().Add(parameterStorage);
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                YMessageBox.Error(exception.Message);
            }
        }

        private void LoadParameters()
        {
            EFCFModel.Entities.Patient patient =
                _dbContext.Set<EFCFModel.Entities.Patient>().FirstOrDefault(pa => pa.Id == _id);
            IList<Parameter> parameters =
                _dbContext.Set<Parameter>().Where(p => p.Patients.Any(pa => pa.Id == _id)).ToList();
            _storages = new ParameterStorage[parameters.Count()];
            for (int i = 0; i < parameters.Count(); i++)
            {
                int index = i;
                Parameter parameter = parameters[i];
                _storages[index] = new ParameterStorage
                                  {
                                      Date = DateTime.Now,
                                      Parameter = parameter,
                                      Patient = patient
                                  };
                Control control = null;
                if (parameter is StringParameter)
                {
                    string text = parameter.DefaultValue != null
                                      ? Encoding.UTF8.GetString(parameter.DefaultValue)
                                      : string.Empty;
                    var c = new TextBox {Name = string.Format("txb{0}", parameter.Name)};
                    c.TextChanged += (sender, e) => _storages[index].Value = Encoding.UTF8.GetBytes(c.Text);
                    c.Text = text;
                    control = c;
                }
                if (parameter is BoolParameter)
                {
                    bool check = parameter.DefaultValue != null
                                     ? BitConverter.ToBoolean(parameter.DefaultValue, 0)
                                     : default(bool);
                    var c = new CheckBox {Name = string.Format("chbx{0}", parameter.Name)};
                    c.CheckedChanged += (sender, e) => _storages[index].Value = BitConverter.GetBytes(c.Checked);
                    c.Checked = check;
                    control = c;
                }
                if (parameter is IntegerParameter)
                {
                    decimal value = parameter.DefaultValue != null
                                        ? BitConverter.ToInt32(parameter.DefaultValue, 0)
                                        : default(decimal);
                    var c = new NumericUpDown
                                {
                                    Name = string.Format("nupd{0}", parameter.Name),
                                    Minimum = (parameter as IntegerParameter).MinValue,
                                    Maximum = (parameter as IntegerParameter).MaxValue
                                };
                    c.ValueChanged += (sender, e) => _storages[index].Value = BitConverter.GetBytes((double) c.Value);
                    c.Value = value;
                    control = c;
                }
                if (parameter is DoubleParameter)
                {
                    decimal value = parameter.DefaultValue != null
                                        ? (decimal) BitConverter.ToDouble(parameter.DefaultValue, 0)
                                        : default(decimal);
                    var c = new NumericUpDown
                                {
                                    Name = string.Format("nupd{0}", parameter.Name),
                                    DecimalPlaces = 2,
                                    Increment = (decimal) 0.5,
                                    Minimum = (decimal) (parameter as DoubleParameter).MinValue,
                                    Maximum = (decimal) (parameter as DoubleParameter).MaxValue
                                };
                    c.ValueChanged += (sender, e) => _storages[index].Value = BitConverter.GetBytes((double)c.Value);
                    c.Value = value;
                    control = c;
                }
                if (parameter is DateTimeParameter)
                {
                    var value = parameter.DefaultValue != null
                                            ? DateTime.FromBinary(BitConverter.ToInt64(parameter.DefaultValue, 0))
                                            : DateTime.Now;
                    var c = new DateTimePicker
                                {
                                    Name = string.Format("dtp{0}", parameter.Name),
                                    /*MaxDate = (parameter as DateTimeParameter).MaxDate,
                                    MinDate = (parameter as DateTimeParameter).MinDate*/
                                };
                    c.ValueChanged += (sender, e) => _storages[index].Value = BitConverter.GetBytes(c.Value.ToBinary());
                    c.Value = value;
                    control = c;
                }
                if (control != null)
                {
                    _labels.Add(parameter.Name);
                    _form.Add(control);
                }
            }
            layoutPanel.ColumnCount = 2;
            layoutPanel.RowCount = _form.Count;
            for (int r = 0; r < layoutPanel.RowCount; r++)
            {
                for (int c = 0; c < layoutPanel.ColumnCount; c += 2)
                {
                    var label = new Label { Text = _labels[r], Top = 5 };
                    layoutPanel.Controls.Add(label, c, r);
                    layoutPanel.Controls.Add(_form[r], c + 1, r);
                }
            }
        }
    }
}
