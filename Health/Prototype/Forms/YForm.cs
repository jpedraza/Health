using System.Windows.Forms;
using EFDAL;

namespace Prototype.Forms
{
    public class YForm : Form
    {
        internal readonly Entities _entities;

        protected YForm(Entities entities) : this()
        {
            _entities = entities;
        }

        protected YForm()
        {
            InitializeComponent();   
        }

        protected virtual void InitializeComponent() {}
    }
}
