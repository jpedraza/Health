using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class PatientsEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public PatientsEditForm()
        {
            InitializeComponent();
        }

        private void PatientsEditFormLoad(object sender, EventArgs e)
        {
            functionalClassesTableAdapter.Fill(healthDatabaseDataSet.FunctionalClasses);
            usersTableAdapter.Fill(healthDatabaseDataSet.Users);
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            usersTableAdapter.Fill(healthDatabaseDataSet.Users);
            functionalClassesTableAdapter.Fill(healthDatabaseDataSet.FunctionalClasses);
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }

        private void PatientsBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                patientsBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }
    }
}
