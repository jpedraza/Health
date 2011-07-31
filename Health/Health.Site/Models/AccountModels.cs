using System.ComponentModel.DataAnnotations;

namespace Health.Site.Models
{
    /// <summary>
    /// Модель формы авторизации на сайте
    /// </summary>
    public class LoginFormModel
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Введите, пожалуйста, пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Необходимо запоминать пользователя?
        /// </summary>
        public bool RememberMe { get; set; }
    }
}