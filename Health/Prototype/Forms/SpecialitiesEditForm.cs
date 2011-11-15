using System;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class SpecialitiesEditForm : Form
    {
        private const string StatusFormat = "Status: {0}.";

        public SpecialitiesEditForm()
        {
            InitializeComponent();
        }

        private void SpecialtiesBindingNavigatorSaveItemClick(object sender, EventArgs e)
        {
            try
            {
                Validate();
                specialtiesBindingSource.EndEdit();
                tableAdapterManager.UpdateAll(healthDatabaseDataSet);
                tsslStatus.Text = string.Format(StatusFormat, "saved on " + DateTime.Now);
            }
            catch (Exception)
            {
                tsslStatus.Text = string.Format(StatusFormat, "sorry, database error.");
            }
        }

        private void SpecialitiesEditFormLoad(object sender, EventArgs e)
        {
            specialtiesTableAdapter.Fill(healthDatabaseDataSet.Specialties);
            tsslStatus.Text = string.Format(StatusFormat, "loaded on" + DateTime.Now);
        }
    }
}
