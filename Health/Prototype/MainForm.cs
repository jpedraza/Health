using Prototype.Forms;
using System;
using System.Windows.Forms;

namespace Prototype
{
    public partial class MainForm : Form
    {
        public MainForm()
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

        private void ShowFormByType<T>()
        {
            Form[] childForms = MdiChildren;
            bool isset = false;
            foreach (var childForm in childForms)
            {
                if (childForm.GetType() == typeof(T))
                {
                    isset = true;
                    childForm.Activate();
                }
            }
            if (!isset)
            {
                var cform = Activator.CreateInstance(typeof(T)) as Form;
                if (cform == null) throw new Exception(String.Format("Невозможно создать окно {0}", typeof(T).Name));
                cform.MdiParent = this;
                cform.Show();
            }
        }

        private void TsmiRolesClick(object sender, EventArgs e)
        {
            ShowFormByType<RolesEditForm>();
        }

        private void TsmiUsersClick(object sender, EventArgs e)
        {
            ShowFormByType<UsersEditForm>();
        }

        private void TsmiCandidatesClick(object sender, EventArgs e)
        {
            ShowFormByType<CandidatesEditForm>();
        }

        private void TsmiPatientsClick(object sender, EventArgs e)
        {
            ShowFormByType<PatientsEditForm>();
        }

        private void TsmiDoctorsClick(object sender, EventArgs e)
        {
            ShowFormByType<DoctorsEditForm>();
        }

        private void TsmiSpecialitiesClick(object sender, EventArgs e)
        {
            ShowFormByType<SpecialitiesEditForm>();
        }

        private void TsmiAppointmentsClick(object sender, EventArgs e)
        {
            ShowFormByType<AppointmentsEditForm>();
        }

        private void TsmiDiagnosisClick(object sender, EventArgs e)
        {
            ShowFormByType<DiagnosisEditForm>();
        }

        private void TsmiDiagnosisClassClick(object sender, EventArgs e)
        {
            ShowFormByType<DiagnosisClassEditForm>();
        }

        private void TsmiFunctionalClassesClick(object sender, EventArgs e)
        {
            ShowFormByType<FunctionalClassEditForm>();
        }

        private void TsmiFunctionalDisordersClick(object sender, EventArgs e)
        {
            ShowFormByType<FunctionalDisordersEditForm>();
        }

        private void TsmiFunctionalDisordersToPatientsClick(object sender, EventArgs e)
        {
            ShowFormByType<FunctionalDisordersToPatientsEditForm>();
        }

        private void TsmiPatientsToDiagnosisClick(object sender, EventArgs e)
        {
            ShowFormByType<PatientsToDiagnosisEditForm>();
        }

        private void TsmiPatientsToDoctorsClick(object sender, EventArgs e)
        {
            ShowFormByType<PatientsToDoctorsEditForm>();
        }

        private void TsmiPatientsToSurgerysClick(object sender, EventArgs e)
        {
            ShowFormByType<PatientsToSurgerysEditForm>();
        }

        private void TsmiSurgerysClick(object sender, EventArgs e)
        {
            ShowFormByType<SurgerysEditForm>();
        }
    }
}
