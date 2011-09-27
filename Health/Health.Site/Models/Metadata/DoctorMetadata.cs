using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Metadata
{
    public class DoctorMetadata
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

        [DisplayName("Токен")]
        public string Token { get; set; }

        [DisplayName("Специальность")]
        public Specialty Specialty { get; set; }
    }

    public class DoctorFormMetadata : DoctorMetadata
    {
        [Required]
        public new string FirstName { get; set; }

        [Required]
        public new string LastName { get; set; }

        [Required]
        public new string Login { get; set; }

        [Required]
        public new string Password { get; set; }

        [Required]
        public new DateTime Birthday { get; set; }
    }
}