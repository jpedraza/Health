using System.Windows.Forms;
using PrototypeHM.DB.DI;

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
        public IDIKernel DIKernel
        {
            get { return _diKernel; }
        }

        #endregion
    }
}
