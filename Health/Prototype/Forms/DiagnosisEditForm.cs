using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class DiagnosisEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public DiagnosisEditForm()
        {
            InitializeComponent();
        }

        private void DiagnosisBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                diagnosisBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void DiagnosisEditFormLoad(object sender, EventArgs e)
        {
            diagnosisClassTableAdapter.Fill(healthDatabaseDataSet.DiagnosisClass);
            diagnosisClassTableAdapter.Fill(healthDatabaseDataSet.DiagnosisClass);
            diagnosisTableAdapter.Fill(healthDatabaseDataSet.Diagnosis);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
