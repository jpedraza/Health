using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class DiagnosisClassEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public DiagnosisClassEditForm()
        {
            InitializeComponent();
        }

        private void DiagnosisClassBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                diagnosisClassBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void DiagnosisClassEditFormLoad(object sender, EventArgs e)
        {
            diagnosisClassTableAdapter.Fill(healthDatabaseDataSet.DiagnosisClass);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
