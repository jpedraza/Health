using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class SurgerysEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public SurgerysEditForm()
        {
            InitializeComponent();
        }

        private void SurgerysBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                surgerysBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void SurgerysEditFormLoad(object sender, EventArgs e)
        {
            surgerysTableAdapter.Fill(healthDatabaseDataSet.Surgerys);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
