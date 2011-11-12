using System;
using System.Linq;
using System.Windows.Forms;
using EFDAL;

namespace Prototype.Forms
{
    public partial class AuthorizationForm : YForm
    {
        private bool _isAuth;
        internal MainForm MainForm { get; set; }

        internal delegate void Authorize(bool isAuthorize);
        internal Authorize OnAuthorize;

        internal delegate void ClosedDelegate(bool isAuthorize);
        internal new ClosedDelegate Closed;

        public AuthorizationForm()
        {

        }

        public AuthorizationForm(Entities entities) : base(entities)
        {
            
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

            int count = _entities.Users.Where(a => a.Login == login && a.Password == password).Count();
            _isAuth = count == 1;
            OnAuthorize(_isAuth);
        }

        private void AuthorizationFormFormClosed(object sender, FormClosedEventArgs e)
        {
            Closed(_isAuth);
        }
    }
}
