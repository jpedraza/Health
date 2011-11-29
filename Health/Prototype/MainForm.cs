using System.Collections.Generic;
using Prototype.Forms;
using System;
using System.Windows.Forms;
using Prototype.Parameter;
using Prototype.Parameter.Metadata;
using Prototype.Parameter.UserControls;

namespace Prototype
{
    public partial class MainForm : Form
    {
        private readonly EnumMetadata<AgeDependsAnswer> _metadata;
        private string _data;

        public MainForm()
        {
            InitializeComponent();
            _metadata = new EnumMetadata<AgeDependsAnswer>
                            {
                                Answers = new List<AgeDependsAnswer>
                                              {
                                                  new AgeDependsAnswer
                                                      {
                                                          AnswerType = AnswerType.Text,
                                                          Description = "Answer 1 description",
                                                          DisplayValue = "Answer 1",
                                                          MaxAge = 10,
                                                          MinAge = 0
                                                      },
                                                  new AgeDependsAnswer
                                                      {
                                                          AnswerType = AnswerType.Text,
                                                          Description = "Answer 2 description",
                                                          DisplayValue = "Answer 2",
                                                          MaxAge = 2,
                                                          MinAge = 0
                                                      }
                                              }
                            };
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
                if (childForm.GetType() == typeof (T))
                {
                    isset = true;
                    childForm.Activate();
                }
            }
            if (!isset)
            {
                var cform = Activator.CreateInstance(typeof (T)) as Form;
                if (cform == null) throw new Exception(String.Format("Невозможно создать окно {0}", typeof (T).Name));
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


        private void рабочийДеньДоктораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFormByType<WorkWeeksEditForm>();
        }

        private void TsmiDeserializationMetadataClick(object sender, EventArgs e)
        {
            var parameterFactory = new ParameterFactory();
            object data = parameterFactory.Deserialize(_data, _metadata.GetType());
            MessageBox.Show(data.ToString(), @"Serialization data");
        }

        private void TsmiSerializationMetadataClick(object sender, EventArgs e)
        {
            var parameterFactory = new ParameterFactory();
            string data = parameterFactory.Serialize(_metadata);
            _data = data;
            MessageBox.Show(data, @"Serialization data");
        }

        private void AnswerTypeControlToolStripMenuItemClick(object sender, EventArgs e)
        {
            var form = new Form
                            {
                                MdiParent = this
                            };
            var enumMetadataControl = new EnumMetadataControl {Dock = DockStyle.Fill};
            form.Controls.Add(enumMetadataControl);
            form.Show();

        }
    }
}
