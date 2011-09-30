using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;

namespace Health.Site.Models.Metadata
{
    public class CandidateMetadata
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

        [DisplayName("Номер полюса")]
        public virtual string Policy { get; set; }

        [DisplayName("Номер больничной карты")]
        public virtual string Card { get; set; }
    }

    public class CandidateFormMetadata : CandidateMetadata
    {
        [Required(ErrorMessage = "Укажите имя")]
        public override string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        public override string LastName { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        public override string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        public override string Password { get; set; }

        [Required(ErrorMessage = "Укажите день рождения")]
        public override DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Укажите номер полюса")]
        public override string Policy { get; set; }

        [Required(ErrorMessage = "Укажите номер больничной карты")]
        public override string Card { get; set; }
    }
}