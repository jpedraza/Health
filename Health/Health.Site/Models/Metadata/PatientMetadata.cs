using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Metadata
{
    public class PatientMetadata
    {
        [DisplayName("Имя")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        [DisplayName("Логин")]
        [Required]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Роль")]
        public Role Role { get; set; }

        [DisplayName("День рождения")]
        [Required]
        public DateTime Birthday { get; set; }

        [DisplayName("Номер полюса")]
        [Required]
        public string Policy { get; set; }

        [DisplayName("Номер больничной карты")]
        [Required]
        public string Card { get; set; }
    }
}