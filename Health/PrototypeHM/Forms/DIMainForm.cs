using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using PrototypeHM.DB.DI;
using PrototypeHM.Diagnosis;
using PrototypeHM.Doctor;
using PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes;
using PrototypeHM.Specialty;
using PrototypeHM.User;
using PrototypeHM.Parameter;
using PrototypeHM.DB;

namespace PrototypeHM.Forms
{
    public partial class DIMainForm : DIForm
    {
        public DIMainForm()
        {
            InitializeComponent();
        }

        public DIMainForm(IDIKernel diKernel) : base(diKernel)
        {
            InitializeComponent();
            
        }

        private void CascadeToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void TsmiDoctorsClick(object sender, EventArgs e)
        {
            var listForm = DIKernel.Get<ListForm<DoctorFullData>>();
            listForm.MdiParent = this;
            /*listForm.LoadData = DIKernel.Get<DoctorRepository>().GetAllPatients;
            listForm.DetailData = DIKernel.Get<DoctorRepository>().Detail;
            listForm.DeleteData = DIKernel.Get<DoctorRepository>().Delete;*/
            listForm.InitializeOperations();
            listForm.Show();
            listForm.Activate();
        }

        private void TsmiUserClick(object sender, EventArgs e)
        {
            var listForm = DIKernel.Get<ListForm<UserFullData>>();
            listForm.MdiParent = this;
            listForm.LoadData = DIKernel.Get<UserRepository>().GetAll;
            listForm.InitializeOperations();
            listForm.Show();
            listForm.Activate();
        }

        private void TsmiDiagnosisClick(object sender, EventArgs e)
        {
            var listForm = DIKernel.Get<ListForm<DiagnosisFullData>>();
            listForm.MdiParent = this;
            listForm.LoadData = DIKernel.Get<DiagnosisRepository>().GetAll;
            listForm.InitializeOperations();
            listForm.Show();
            listForm.Activate();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddForm<DoctorDetail>(DIKernel) { MdiParent = this };
            form.InitializeForm();
            form.Show();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        //private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var listForm = new ListForm<ParameterDetail>(DIKernel);
        //    listForm.MdiParent = this;
        //    listForm.LoadData = DIKernel.Get<ParameterRepository>().GetAllParameters;
        //    listForm.InitializeOperations();
        //    listForm.Show();
        //    listForm.Activate();
        //}

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editForm = new AddForm<ParameterDetail>(DIKernel, -1) { MdiParent = this };
            
            editForm.InitializeForm();
            editForm.Show();
        }

        

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editForm = new AddForm<MetadataForParameter>(DIKernel, -1) {MdiParent = this};
            editForm.InitializeForm();
            editForm.Show();
        }

        private void ���������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editForm = new AddForm<DoctorDetail>(DIKernel, -1) { MdiParent = this };
            editForm.InitializeForm();
            editForm.Show();
        }

        private void �������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editForm = new AddForm<ValueTypeOfMetadata>(DIKernel, -1) { MdiParent = this };
            editForm.InitializeForm();
            editForm.Show();
        }

        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var form = new PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes.ListForm(DIKernel) { MdiParent = this };
                form.Show();
            form = null;

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes.AddForm(this.DIKernel);
            form.ShowDialog();
        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new PrototypeHM.Forms.EntitysForm.ParameterForms.MetaData.ListForm(DIKernel)
                               {MdiParent = this};
                form.Show();
                form = null;
            }
            catch(Exception exp)
            {
                YMessageBox.Error(exp.Message);
            }
            
        }

        private void �����������������ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            try
            {
                var form = new PrototypeHM.Forms.EntitysForm.ParameterForms.Parameter.ListParametersForm(DIKernel) { MdiParent = this };
                form.Show();
                form = null;
            }
            catch (Exception exp)
            {
                YMessageBox.Error(exp.Message);
            }
        }
    }
}
