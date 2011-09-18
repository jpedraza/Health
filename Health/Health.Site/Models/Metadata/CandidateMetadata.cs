using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Metadata
{
    public class CandidateMetadata
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [DisplayName("Идентификатор кандидата")]
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DisplayName("Имя")]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [DisplayName("Фимилия")]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DisplayName("Логин")]
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [DisplayName("Роль")]
        public Role Role { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        [DisplayName("День Рождения")]
        [Required]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Токен для сессии в куках
        /// </summary>
        [DisplayName("Токен")]
        public string Token { get; set; }

        /// <summary>
        /// Номер полюса.
        /// </summary>
        [DisplayName("Номер полюса")]
        [Required]
        public string Policy { get; set; }

        /// <summary>
        /// Номер больничной карты.
        /// </summary>
        [DisplayName("Номер больничной карты")]
        [Required]
        public string Card { get; set; }
    }
}