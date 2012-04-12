using System;
using System.Windows.Forms;
using Model.Entities;

namespace Prototype.Parameters
{
    internal class DoubleRenderer : IRenderer
    {
        private NumericUpDown _c;

        public Control Render(Parameter parameter)
        {
            if (!(parameter is DoubleParameter))
                throw new Exception(string.Format("Тип параметра должен быть {0}", typeof(DoubleParameter)));

            decimal value = parameter.DefaultValue != null
                                        ? (decimal)BitConverter.ToDouble(parameter.DefaultValue, 0)
                                        : default(decimal);
            _c = new NumericUpDown
                     {
                         Name = string.Format("nupd{0}", parameter.Name),
                         DecimalPlaces = 2,
                         Increment = (decimal) 0.5,
                         Minimum = (decimal) (parameter as DoubleParameter).MinValue,
                         Maximum = (decimal) (parameter as DoubleParameter).MaxValue,
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
