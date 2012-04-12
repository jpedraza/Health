using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using Model.Entities;
using Prototype.DI;
using Prototype.Parameters;

namespace Prototype.Forms.Patient
{
    public partial class EnterParameterForm : DIForm
    {
        private readonly int _id;
        private readonly DbContext _dbContext;
        private ParameterStorage[] _storages;
        private readonly IList<Control> _form;
        private readonly IList<string> _labels;
        private readonly RenderFactory _renderFactory;

        public EnterParameterForm(IDIKernel diKernel, int id) : base(diKernel)
        {
            _id = id;
            _form = new BindingList<Control>();
            _labels = new BindingList<string>();
            _dbContext = Get<DbContext>();
            _renderFactory = Get<RenderFactory>();
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
            Model.Entities.Patient patient =
                _dbContext.Set<Model.Entities.Patient>().FirstOrDefault(pa => pa.Id == _id);
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

                try
                {
                    IRenderer renderer = _renderFactory.Renderer(parameter.GetType().BaseType);
                    Control control = renderer.Render(parameter);
                    renderer.Changed(_storages[index]);
                    _labels.Add(parameter.Name);
                    _form.Add(control);
                }
                catch (Exception e)
                {
                    YMessageBox.Error(e.Message);
                }
            }
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
        }
    }
}
