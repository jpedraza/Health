using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Users"), ScaffoldTable(true), DisplayName("Пользователь")]
    public class User : IIdentity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [Required, DisplayName("Имя")]
        public string FirstName { get; set; }

        [Required, DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        [Required, DisplayName("Логин")]
        public string Login { get; set; }

        [Required, DisplayName("Пароль")]
        public string Password { get; set; }

        [Required, DisplayName("День рождения")]
        public DateTime Birthday { get; set; }

        [NotDisplay, DisplayName("Роль")]
        public virtual Role Role { get; set; }
    }
}