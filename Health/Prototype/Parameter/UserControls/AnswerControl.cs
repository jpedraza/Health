using System.Windows.Forms;
using Prototype.Parameter.Metadata;

namespace Prototype.Parameter.UserControls
{
    public partial class AnswerControl : UserControl
    {
        protected readonly AnswerValueControl _answerValueControl;

        public virtual Answer Answer
        {
            get
            {
                return new Answer
                           {
                               AnswerType = (AnswerType) tscbAnswerValueType.SelectedIndex,
                               Description = txtDescription.Text,
                               DisplayValue = txtDisplayValue.Text,
                               Value = _answerValueControl.Value
                           };
            }
        }

        public AnswerControl()
        {
            _answerValueControl = new AnswerValueControl
                                      {
                                          AnswerType = AnswerType.Number
                                      };
            InitializeComponent();
            pAnswerValue.Controls.Add(_answerValueControl);
            tscbAnswerValueType.SelectedIndex = 1;
        }

        private void TscbAnswerValueTypeSelectedIndexChanged(object sender, System.EventArgs e)
        {
            _answerValueControl.AnswerType = (AnswerType) tscbAnswerValueType.SelectedIndex;
        }
    }
}
