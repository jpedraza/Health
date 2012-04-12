using System;
using System.Text;
using System.Windows.Forms;
using Model.Entities;

namespace Prototype.Parameters
{
    internal class StringRenderer : IRenderer
    {
        private TextBox _c;

        public Control Render(Parameter parameter)
        {
            if (!(parameter is StringParameter))
                throw new Exception(string.Format("Тип параметра должен быть {0}", typeof(StringParameter)));

            string text = parameter.DefaultValue != null
                                      ? Encoding.UTF8.GetString(parameter.DefaultValue)
                                      : string.Empty;
            _c = new TextBox {Name = string.Format("txb{0}", parameter.Name), Text = text};
            return _c;
        }

        public void Changed(ParameterStorage storage)
        {
            _c.TextChanged += (sender, e) => storage.Value = Encoding.UTF8.GetBytes(_c.Text);
        }
    }
}
