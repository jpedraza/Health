using System.Windows.Forms;
using EFDAL;
using Prototype.Forms;

namespace Prototype
{
    internal class YApplication : ApplicationContext
    {
        internal MainForm _mainForm;
        internal readonly AuthorizationForm _authorizationForm;
        internal Entities _entities;

        internal YApplication()
        {
            _entities = new Entities();
            _authorizationForm = new AuthorizationForm(_entities)
                                     {
                                         OnAuthorize = OnAuthorize,
                                         Closed = OnAuthorizationFormClosed,
                                         StartPosition = FormStartPosition.CenterScreen
                                     };
            _authorizationForm.Show();
        }

        internal void OnAuthorizationFormClosed(bool isAuthorize)
        {
            if (_mainForm != null) _mainForm.Close();
            if (!isAuthorize)
            {
                ExitThread();
                Application.Exit();
            }
        }

        internal void OnAuthorize(bool isAuthorize)
        {
            if (isAuthorize)
            {
                _authorizationForm.Close();
                _mainForm = new MainForm(_entities)
                                {
                                    StartPosition = FormStartPosition.CenterScreen
                                };
                MainForm = _mainForm;
                _mainForm.Show();
            }
        }
    }
}
