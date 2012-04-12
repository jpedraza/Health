using System.Windows.Forms;
using Model.Entities;

namespace Prototype.Parameters
{
    internal interface IRenderer
    {
        Control Render(Parameter parameter);

        void Changed(ParameterStorage storage);
    }
}
