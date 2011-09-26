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
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        [DisplayName("Полное имя")]
        public string FullName { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Роль")]
        public Role Role { get; set; }

        [DisplayName("День рождения")]
        public DateTime Birthday { get; set; }

        [DisplayName("Номер полюса")]
        public string Policy { get; set; }

        [DisplayName("Номер больничной карты")]
        public string Card { get; set; }
    }

    public class CandidateFormMetadata : CandidateMetadata
    {
        [Required(ErrorMessage = "Укажите имя")]
        public new string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        public new string LastName { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        public new string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        public new string Password { get; set; }

        [Required(ErrorMessage = "Укажите день рождения")]
        public new DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Укажите номер полюса")]
        public new string Policy { get; set; }

        [Required(ErrorMessage = "Укажите номер больничной карты")]
        public new string Card { get; set; }
    }
}