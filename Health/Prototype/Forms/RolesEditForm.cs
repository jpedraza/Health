using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class RolesEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public RolesEditForm()
        {
            InitializeComponent();
        }

        private void RolesBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                rolesBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void RolesEditFormLoad(object sender, EventArgs e)
        {
            rolesTableAdapter.Fill(healthDatabaseDataSet.Roles);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
