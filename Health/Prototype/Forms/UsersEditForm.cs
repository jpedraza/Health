using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class UsersEditForm : Form
    {
        private const string StatusFormat = "Status: {0}";

        public UsersEditForm()
        {
            InitializeComponent();
        }

        private void UsersBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try { 
                Validate();
                usersBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void UsersEditFormLoad(object sender, EventArgs e)
        {
            rolesTableAdapter.Fill(healthDatabaseDataSet.Roles);
            usersTableAdapter.Fill(healthDatabaseDataSet.Users);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
