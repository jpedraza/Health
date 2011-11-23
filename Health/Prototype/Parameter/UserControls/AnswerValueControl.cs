using System;
using System.Windows.Forms;
using Prototype.Parameter.Metadata;

namespace Prototype.Parameter.UserControls
{
    public partial class AnswerValueControl : UserControl
    {
        private AnswerType _answerType;
        private Control _control;

        public AnswerType AnswerType
        {
            get { return _answerType; }
            set
            {
                _answerType = value;
                Controls.Clear();
                Render();
            }
        }

        public object Value
        {
            get { return _control.Text; }
            set { _control.Text = value.ToString(); }
        }

        public AnswerValueControl()
        {
            InitializeComponent();
        }

        private void Render()
        {
            if (AnswerType == AnswerType.Text)
            {
                _control = new TextBox();
            }
            if (AnswerType == AnswerType.Number)
            {
                _control = new NumericUpDown();
            }
            if (_control == null) throw new Exception("Тип данных для значения не поддерживается.");
            _control.Dock = DockStyle.Left;
            _control.Text = @"Answer value";
            Controls.Add(_control);
        }
    }
}
