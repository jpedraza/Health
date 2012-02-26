using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EFCFModel.Entities;
using PrototypeHM.DI;

namespace PrototypeHM.Forms.Patient
{
    public partial class EnterParameterForm : DIForm
    {
        private readonly int _patientId;
        private readonly DbContext _dbContext;

        protected EnterParameterForm()
        {
            InitializeComponent();
        }

        public EnterParameterForm(IDIKernel diKernel, int patientId) : base(diKernel)
        {
            _patientId = patientId;
            _dbContext = Get<DbContext>();
            InitializeComponent();
            LoadParameters();
        }

        private void LoadParameters()
        {
            IEnumerable<Parameter> parameters =
                _dbContext.Set<Parameter>().Where(p => p.Patients.Any(pa => pa.Id == _patientId));
            foreach (Parameter parameter in parameters)
            {
                if (parameter is StringParameter)
                {
                    
                }
                if (parameter is BoolParameter)
                {
                    
                }
                if (parameter is IntegerParameter)
                {
                    
                }
                if (parameter is DoubleParameter)
                {
                    
                }
            }
        }
    }
}
