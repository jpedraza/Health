using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;
using PrototypeHM.Forms.EntitysForm.ParameterForms.Other;
using PrototypeHM.Parameter;
using System.Data.SqlClient;

namespace PrototypeHM.Forms.EntitysForm.AdminForms
{
    public partial class PatientListForm : DIForm, ICommonFormsFunctions
    {
        public PatientListForm(IDIKernel diKernel, int doctorId)
            : base(diKernel)
        {
            InitializeComponent();
            _doctorId = doctorId;
        }

        private IList<Doctor.PatientForDoctor> _listData;
        private int _doctorId;
        private Doctor.DoctorFullData _doctorFullData;

        private ListBox _parameters;
        private Label _infoLabel;
        private ListBox _diagnosis;

        private bool _renderingFlag = false;
        private void LinkLabelUpdateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void PatientListFormLoad(object sender, EventArgs e)
        {
            try
            {
                this.SwitchOnNoticePanel();
                GetPatients();
                this.GetDoctorInfo();
                this.GetPositiveNotice("Данные успешно заполнены");
            }
            catch(Exception exp)
            {
                this.GetNegativeNotice(exp.Message);
            }
        }

        private void GetPatients()
        {
            var query = string.Format("EXEC [dbo].[GetAllPatientsForDoctor] {0}", _doctorId);
            var sqlCommand = DIKernel.Get<DB.DB>().Connection.CreateCommand();
            sqlCommand.CommandText = query;

            var reader = sqlCommand.ExecuteReader();
            _listData = Get<PropertyToColumnMapper<Doctor.PatientForDoctor>>().Map(reader);
            reader.Close();
            foreach (var stroka in 
                _listData.Select(patientFullData =>
                    string.Format("{0} {1} {2}. {3}. [{4}]",
                    patientFullData.Id, patientFullData.LastName, patientFullData.FirstName.Substring(0, 1), 
                    patientFullData.ThirdName.Substring(0,1), patientFullData.Mother)))
            {
                PatientsList.Items.Add(stroka);
            }
        }

        private void GetDoctorInfo()
        {
            var query = string.Format("EXEC [dbo].[GetDoctorShowData] {0}", _doctorId);
            var sqlCommand = DIKernel.Get<DB.DB>().Connection.CreateCommand();
            sqlCommand.CommandText = query;

            var reader = sqlCommand.ExecuteReader();
            _doctorFullData = Get<PropertyToColumnMapper<Doctor.DoctorFullData>>().Map(reader)[0];
            reader.Close();

            label2.Text = string.Format("Здраствуйте, {0} {1}. {2}.", _doctorFullData.LastName,
                                        this.GetFirstLetterInString(_doctorFullData.FirstName),
                                        this.GetFirstLetterInString(_doctorFullData.ThirdName));
        }

        private void renderRightForm()
        {
            label1.Text = string.Empty;
            _infoLabel = new Label
                                {
                                    Text = "test1\ntest2\ntest3",
                                    Name = "infoLabel",
                                    Location = new Point(10, 20),
                                    AutoSize = true
                                };

            groupBox1.Controls.Add(_infoLabel);

            _parameters = new ListBox
                                 {
                                     Height = 150,
                                     Width = groupBox1.Width - 2*_infoLabel.Location.X,
                                     Location =
                                         new Point(_infoLabel.Location.X, _infoLabel.Height + _infoLabel.Location.Y + 30),
                                     Name = "parameters"
                                 };

            groupBox1.Controls.Add(_parameters);


            _diagnosis = new ListBox
                                {
                                    Name = "diagnosis",
                                    Height = _parameters.Height,
                                    Width = groupBox1.Width - 2*_infoLabel.Location.X,
                                    Location =
                                         new Point(_infoLabel.Location.X, _parameters.Height + _parameters.Location.Y + 30)
                                };

            groupBox1.Controls.Add(_diagnosis);
        }

        private void PatientsListMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(!_renderingFlag)
                {
                    renderRightForm();
                    _renderingFlag = true;
                }
                for(var i=0; i<PatientsList.Items.Count; i++)
                {
                    if(PatientsList.SelectedIndex == i)
                    {
                        renderPatient(_listData[i]);
                    }
                }  
            }
        }

        private void renderPatient(Doctor.PatientForDoctor patient)
        {
            _infoLabel.Text =
                string.Format("ФИО: {0}\n{1} {2} \nродитель: {3}",
                patient.LastName,
                patient.FirstName,
                patient.ThirdName,
                patient.Mother
                );
        }
    }
}
