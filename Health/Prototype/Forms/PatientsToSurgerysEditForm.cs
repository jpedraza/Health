using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class PatientsToSurgerysEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public PatientsToSurgerysEditForm()
        {
            InitializeComponent();
        }

        private void PatientsToSurgerysBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                patientsToSurgerysBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void PatientsToSurgerysEditFormLoad(object sender, EventArgs e)
        {
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            surgerysTableAdapter.Fill(healthDatabaseDataSet.Surgerys);
            patientsToSurgerysTableAdapter.Fill(healthDatabaseDataSet.PatientsToSurgerys);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
