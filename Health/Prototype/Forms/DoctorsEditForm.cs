using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class DoctorsEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public DoctorsEditForm()
        {
            InitializeComponent();
        }

        private void DoctorsBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                doctorsBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void DoctorsEditFormLoad(object sender, EventArgs e)
        {
            specialtiesTableAdapter.Fill(healthDatabaseDataSet.Specialties);
            usersTableAdapter.Fill(healthDatabaseDataSet.Users);
            doctorsTableAdapter.Fill(healthDatabaseDataSet.Doctors);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
