using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class FunctionalDisordersToPatientsEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public FunctionalDisordersToPatientsEditForm()
        {
            InitializeComponent();
        }

        private void FunctionalDisordersToPatientsBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                functionalDisordersToPatientsBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void FunctionalDisordersToPatientsEditFormLoad(object sender, EventArgs e)
        {
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            functionalDisordersTableAdapter.Fill(healthDatabaseDataSet.FunctionalDisorders);
            functionalDisordersToPatientsTableAdapter.Fill(healthDatabaseDataSet.FunctionalDisordersToPatients);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
