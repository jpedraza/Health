using System;
using System.Windows.Forms;
using Model.Entities;

namespace Prototype.Parameters
{
    internal class BoolRenderer : IRenderer
    {
        private CheckBox _c; 

        public Control Render(Parameter parameter)
        {
            if (!(parameter is BoolParameter))
                throw new Exception(string.Format("Тип параметра должен быть {0}", typeof (BoolParameter)));

            bool check = parameter.DefaultValue != null
                                    ? BitConverter.ToBoolean(parameter.DefaultValue, 0)
                                    : default(bool);
            _c = new CheckBox {Name = string.Format("chbx{0}", parameter.Name), Checked = check};
            return _c;
        }

        public void Changed(ParameterStorage storage)
        {
            _c.CheckedChanged += (sender, e) => storage.Value = BitConverter.GetBytes(_c.Checked);
        }
    }
}
