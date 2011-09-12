using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Models.Forms;

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
    public class RegistrationFormModel : Core.Entities.POCO.Candidate
    {
        [Required]
        public new string FirstName { get; set; }

        [Required]
        public new string LastName { get; set; }

        [Required]
        public new string ThirdName { get; set; }

        [Required]
        public new string Login { get; set; }

        [Required]
        public new string Password { get; set; }

        [NotMapped]
        public new Role Role { get; set; }

        [Required]
        public new DateTime Birthday { get; set; }

        [NotMapped]
        public new string Token { get; set; }

        [Required]
        public new string Policy { get; set; }

        [Required]
        public new string Card { get; set; }
    }

    /// <summary>
    /// Форма опроса пользователя при первом входе в систему
    /// </summary>
    public class InterviewFormModel //: ParametersFormBase, IValidatableObject
    {
        /*public InterviewFormModel(IDIKernel di_kernel) : base(di_kernel)
        {
        }*/

        public IList<Parameter> Parameters { get; set; }

        #region IValidatableObject Members

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validation_context)
        {
            List<Parameter> param = Parameters.ToList();
            var result = new List<ValidationResult>();
            for (int i = 0; i < param.Count; i++)
            {
                var data_type_attr = new DataTypeAttribute(DataType.EmailAddress);
                var v_result = new RequiredAttribute
                                   {
                                       ErrorMessage = "Значение параметра не может быть пустым."
                                   };
                if (!v_result.IsValid(Parameters.ElementAt(i).Value))
                {
                    result.Add(new ValidationResult(
                                   v_result.ErrorMessage,
                                   new[]
                                       {
                                           String.Format("Parameters[{0}].Value", i)
                                       }
                                   ));
                }
            }
            return result;
        }*/

        #endregion
    }
}