using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Metadata
{
    public class DoctorMetadata
    {
        [DisplayName("#")]
        public virtual int Id { get; set; }

        [DisplayName("Имя")]
        public virtual string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public virtual string LastName { get; set; }

        [DisplayName("Отчество")]
        public virtual string ThirdName { get; set; }

        [DisplayName("Полное имя")]
        public virtual string FullName { get; set; }

        [DisplayName("Логин")]
        public virtual string Login { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [DisplayName("Роль")]
        public virtual Role Role { get; set; }

        [DisplayName("День рождения")]
        public virtual DateTime Birthday { get; set; }

        [DisplayName("Токен")]
        public virtual string Token { get; set; }

        [DisplayName("Специальность")]
        public virtual Specialty Specialty { get; set; }

        [DisplayName("Ведомые пациенты")]
        public virtual ICollection<Patient> Patients { get; set; }
    }

    public class DoctorFormMetadata : DoctorMetadata
    {
        [Required]
        public override string FirstName { get; set; }

        [Required]
        public override string LastName { get; set; }

        [Required]
        public override string Login { get; set; }

        [Required]
        public override string Password { get; set; }

        [Required]
        public override DateTime Birthday { get; set; }
    }
}