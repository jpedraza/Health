using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class WorkWeeksEditForm : Form
    {
        public WorkWeeksEditForm()
        {
            InitializeComponent();
        }

        private void workWeeksBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.workWeeksBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.healthDatabaseDataSet);
            }
            catch(Exception exp)
            {
                MessageBox.Show("Скорее всего вы задали несколько расписаний работы \n для одного рабочего дня доктора", exp.Message);
            }
        }

        private void WorkWeeksEditForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "healthDatabaseDataSet.WorkWeeks". При необходимости она может быть перемещена или удалена.
            this.workWeeksTableAdapter.Fill(this.healthDatabaseDataSet.WorkWeeks);

        }
    }
}
