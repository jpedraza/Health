using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class PatientsToDiagnosisEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public PatientsToDiagnosisEditForm()
        {
            InitializeComponent();
        }

        private void PatientsToDiagnosisBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                patientsToDiagnosisBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void PatientsToDiagnosisEditFormLoad(object sender, EventArgs e)
        {
            diagnosisTableAdapter.Fill(healthDatabaseDataSet.Diagnosis);
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            patientsToDiagnosisTableAdapter.Fill(healthDatabaseDataSet.PatientsToDiagnosis);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
