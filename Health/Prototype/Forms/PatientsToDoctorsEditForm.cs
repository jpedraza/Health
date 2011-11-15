using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class PatientsToDoctorsEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public PatientsToDoctorsEditForm()
        {
            InitializeComponent();
        }

        private void PatientsToDoctorsBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                patientsToDoctorsBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void PatientsToDoctorsEditFormLoad(object sender, EventArgs e)
        {
            doctorsTableAdapter.Fill(healthDatabaseDataSet.Doctors);
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            patientsToDoctorsTableAdapter.Fill(healthDatabaseDataSet.PatientsToDoctors);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
