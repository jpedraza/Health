using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Prototype.Parameter.Metadata;

namespace Prototype.Parameter.UserControls
{
    public partial class EnumMetadataControl : UserControl
    {
        public List<Answer> Answers { get; set; }
        private readonly List<AnswerControl> _answerControls;
        private readonly IMetadata _metadata;

        public EnumMetadataControl()
        {
            Answers = new List<Answer>();
            _answerControls = new List<AnswerControl>();
            InitializeComponent();
            Render();
        }

        public EnumMetadataControl(EnumMetadata<Answer> metadata) : this()
        {
            _metadata = metadata;
        }

        private void Render()
        {
        }

        private void TsbAddAnswerClick(object sender, EventArgs e)
        {
            Control control = null;
            if (tscbAnswerType.SelectedIndex == 1)
            {
                control = new AnswerControl();
                Answers.Add(new Answer());
            }
            if (tscbAnswerType.SelectedIndex == 0)
            {
                control = new AgeDependsAnswerControl();
                Answers.Add(new AgeDependsAnswer());
            }
            if (control == null)
                throw new Exception("Не поддерживается тип для компонента.");
            int height = 0;
            foreach (AnswerControl answerControl in _answerControls)
            {
                height += answerControl.Height;
            }
            control.Top = height;
            _answerControls.Add(control as AnswerControl);
            pAnswerControls.Controls.Clear();
            pAnswerControls.Controls.AddRange(_answerControls.ToArray());
        }
    }
}
