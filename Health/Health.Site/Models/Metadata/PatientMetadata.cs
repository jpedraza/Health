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
        [DisplayName("Идентификатор пользователя")]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Укажите фамилию")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Укажите логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }

        [DisplayName("Роль")]
        public Role Role { get; set; }

        [DisplayName("День рождения")]
        [Required(ErrorMessage = "Укажите день рождения")]
        public DateTime Birthday { get; set; }

        [DisplayName("Номер полюса")]
        [Required(ErrorMessage = "Укажите номер полюса")]
        public string Policy { get; set; }

        [DisplayName("Номер больничной карты")]
        [Required(ErrorMessage = "Укажите номер больничной карты")]
        public string Card { get; set; }
    }

    public class IfSubPatientMetadata : PatientMetadata
    {
        [Required(ErrorMessage = "Выберите пользователя.")]
        public new int Id { get; set; }
    }
}