using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class AppointmentsEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public AppointmentsEditForm()
        {
            InitializeComponent();
        }

        private void AppointmentsBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                appointmentsBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void AppointmentsEditFormLoad(object sender, EventArgs e)
        {
            doctorsTableAdapter.Fill(healthDatabaseDataSet.Doctors);
            patientsTableAdapter.Fill(healthDatabaseDataSet.Patients);
            appointmentsTableAdapter.Fill(healthDatabaseDataSet.Appointments);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
