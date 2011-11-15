using System.Windows.Forms;

namespace Prototype.Forms
{
    public partial class AuthorizationForm : Form
    {
        public delegate string YAuthorize(string login, string password);

        public YAuthorize OnAuthorize;

        public delegate void YClose();

        public YClose OnClose;

        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void BtnLoginClick(object sender, System.EventArgs e)
        {
            string login = txbLogin.Text;
            string password = txbPassword.Text;
            string status = OnAuthorize(login, password);
            const string format = "Status: {0}";
            tsslLoginStatus.Text = string.Format(format, status);
        }

        private void AuthorizationFormFormClosed(object sender, FormClosedEventArgs e)
        {
            OnClose();
        }
    }
}
