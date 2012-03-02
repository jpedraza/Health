using System.ComponentModel;
using System.Windows.Forms;
using PrototypeHM.DI;

namespace PrototypeHM.Forms
{
    public class DIForm : Form, IDIInjected
    {
        public string UID { get; set; }

        private readonly IDIKernel _diKernel;
        [Browsable(false)]
        public IDIKernel DIKernel { get { return _diKernel; } }

        public DIForm() { }

        public DIForm(IDIKernel diKernel) : this()
        {
            _diKernel = diKernel;
        }

        protected T Get<T>()
        {
            return DIKernel.Get<T>();
        }

        protected T Get<T>(params ConstructorArgument[] arguments)
            where T : class 
        {
            return DIKernel.Get(typeof (T), arguments) as T;
        }
    }
}