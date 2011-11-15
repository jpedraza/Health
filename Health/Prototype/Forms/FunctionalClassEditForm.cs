using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class FunctionalClassEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public FunctionalClassEditForm()
        {
            InitializeComponent();
        }

        private void FunctionalClassesBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                functionalClassesBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void FunctionalClassEditFormLoad(object sender, EventArgs e)
        {
            functionalClassesTableAdapter.Fill(healthDatabaseDataSet.FunctionalClasses);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
