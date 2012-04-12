using System;
using System.Windows.Forms;
using Model.Entities;

namespace Prototype.Parameters
{
    internal class IntegerRenderer : IRenderer
    {
        private NumericUpDown _c;

        public Control Render(Parameter parameter)
        {
            if (!(parameter is IntegerParameter))
                throw new Exception(string.Format("Тип параметра должен быть {0}", typeof(IntegerParameter)));

            decimal value = parameter.DefaultValue != null
                                        ? BitConverter.ToInt32(parameter.DefaultValue, 0)
                                        : default(decimal);
            _c = new NumericUpDown
                     {
                         Name = string.Format("nupd{0}", parameter.Name),
                         Minimum = (parameter as IntegerParameter).MinValue,
                         Maximum = (parameter as IntegerParameter).MaxValue,
                         Value = value
                     };
            return _c;
        }

        public void Changed(ParameterStorage storage)
        {
            _c.ValueChanged += (sender, e) => storage.Value = BitConverter.GetBytes((double)_c.Value);
        }
    }
}
