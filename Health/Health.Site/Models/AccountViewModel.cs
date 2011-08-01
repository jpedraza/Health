using Health.Site.Models.Forms;

namespace Health.Site.Models
{
    /// <summary>
    /// Модель представлений AccountController
    /// </summary>
    public class AccountViewModel : CoreViewModel
    {
        /// <summary>
        /// Форма входа
        /// </summary>
        public LoginFormModel LoginForm { get; set; }

        /// <summary>
        /// Форма регистрации кандидатов
        /// </summary>
        public RegistrationFormModel RegistrationForm { get; set; }

        /// <summary>
        /// Форма опроса пользователя при первом входе в систему
        /// </summary>
        public InterviewFormModel InterviewForm { get; set; }
    }
}