using System.ComponentModel;
using System.Windows.Forms;
using PrototypeHM.DI;

namespace PrototypeHM.Forms
{
    public class DIForm : Form, IDIInjected
    {
        public DIForm()
        {
        }

        public DIForm(IDIKernel diKernel) : this()
        {
            _diKernel = diKernel;
        }

        protected T Get<T>()
        {
            return DIKernel.Get<T>();
        }

        #region Implementation of IDIInjected

        private readonly IDIKernel _diKernel;

        [Browsable(false)]
        public IDIKernel DIKernel
        {
            get { return _diKernel; }
        }

        #endregion
    }
}