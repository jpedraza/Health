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
using PrototypeHM.Forms.EntitysForm.ParameterForms.Other;
using PrototypeHM.Parameter;

namespace PrototypeHM.Forms.EntitysForm.ParameterForms.Parameter
{
    public partial class AddParameterForm : DIForm, ICommonFormsFunctions
    {
        public AddParameterForm(IDIKernel diKernel)
            : base(diKernel)
        {
            InitializeComponent();
        }

        public bool FlagResult = false;

        private void Button1Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxNameParameter.Text))
                    throw new Exception("Укажите имя параметра");

                if (string.IsNullOrEmpty(textBoxDefaultValue.Text))
                {
                    throw new Exception("Укажите значение по умолчанию");
                }


                var oC = DIKernel.Get<OperationsRepository>().
                Operations.FirstOrDefault(o => o.GetType() == typeof(OperationsContext<ParameterBaseData>)) as
                                                  OperationsContext<ParameterBaseData>;

                if (oC == null)
                    throw new Exception("Отсутствует контекст операции");

                var @delegate = oC.Save;
                if (@delegate == null)
                    throw new Exception("Отсутствует операция сохранения данных");

                var obj = new ParameterBaseData();
                obj.Name = textBoxNameParameter.Text;
                obj.DefaultValue = Encoding.UTF8.GetBytes(textBoxDefaultValue.Text);
                var qS = @delegate(obj);

                if (qS.Status == 1)
                {
                    FlagResult = true;
                    Close();
                }
                else
                {
                    this.GetNegativeNotice(qS.StatusMessage);
                }
            }
            catch (Exception exp)
            {
                this.GetNegativeNotice(exp.Message);
            }
        }

        private void Button2Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AddParameterForm_Load(object sender, EventArgs e)
        {
            this.SwitchOnNoticePanel();
            this.GetPositiveNotice();
        }
    }
}
