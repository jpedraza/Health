using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Health.API;
using Health.API.Entities;
using Health.API.Validators;
using Health.Data.Validators;

namespace Health.Site.Areas.Account.Models.Forms
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

    /// <summary>
    /// Модель формы регистрации кандидатов
    /// </summary>
    public class RegistrationFormModel : ICandidate
    {
        #region ICandidate Members

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ThirdName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        public IRole Role { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [Required]
        public string Policy { get; set; }

        [Required]
        public string Card { get; set; }

        #endregion
    }

    /// <summary>
    /// Форма опроса пользователя при первом входе в систему
    /// </summary>
    public class InterviewFormModel : ParametersFormBase, IValidatableObject
    {
        public InterviewFormModel(IDIKernel di_kernel) : base(di_kernel)
        {
            
        }

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validation_context)
        {
            var result = new List<ValidationResult>();
            var validator_factory = DIKernel.Get<IValidatorFactory>();
            if (!validator_factory.IsValid("Health.Data.Validators.RequiredValidator, Health.Data", Parameters.ToList()[0].Value))
            {
                result.Add(new ValidationResult(validator_factory.Message, new[]
                                                                               {
                                                                                   "Parameters[0].Value"
                                                                               }));
            }
            return result;
        }

        #endregion
    }
}