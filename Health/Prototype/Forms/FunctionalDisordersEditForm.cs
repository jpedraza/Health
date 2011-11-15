using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class FunctionalDisordersEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public FunctionalDisordersEditForm()
        {
            InitializeComponent();
        }

        private void FunctionalDisordersBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                functionalDisordersBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void FunctionalDisordersEditFormLoad(object sender, EventArgs e)
        {
            functionalDisordersTableAdapter.Fill(healthDatabaseDataSet.FunctionalDisorders);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
