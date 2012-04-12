using System;
using System.Windows.Forms;
using Model.Entities;

namespace Prototype.Parameters
{
    internal class DateTimeRenderer : IRenderer
    {
        private DateTimePicker _c;

        public Control Render(Parameter parameter)
        {
            if (!(parameter is DateTimeParameter))
                throw new Exception(string.Format("Тип параметра должен быть {0}", typeof(DateTimeParameter)));

            var value = parameter.DefaultValue != null
                                            ? DateTime.FromBinary(BitConverter.ToInt64(parameter.DefaultValue, 0))
                                            : DateTime.Now;
            _c = new DateTimePicker
                     {
                         Name = string.Format("dtp{0}", parameter.Name),
                         Value = value,
                         //MaxDate = (parameter as DateTimeParameter).MaxDate,
                         //MinDate = (parameter as DateTimeParameter).MinDate
                     };
            return _c;
        }

        public void Changed(ParameterStorage storage)
        {
            _c.ValueChanged += (sender, e) => storage.Value = BitConverter.GetBytes(_c.Value.ToBinary());
        }
    }
}
