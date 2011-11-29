using System;
using Prototype.Parameter.Metadata;

namespace Prototype.Parameter.UserControls
{
    public partial class AgeDependsAnswerControl : AnswerControl
    {
        public override Answer Answer
        {
            get
            {
                return new AgeDependsAnswer
                {
                    AnswerType = (AnswerType)tscbAnswerValueType.SelectedIndex,
                    Description = txtDescription.Text,
                    DisplayValue = txtDisplayValue.Text,
                    Value = _answerValueControl.Value,
                    MinAge = Convert.ToInt32(nupdMinimalAge.Value),
                    MaxAge = Convert.ToInt32(nupdMaximalAge.Value)
                };
            }
        }

        public AgeDependsAnswerControl()
        {
            InitializeComponent();
        }
    }
}
