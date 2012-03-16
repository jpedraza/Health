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

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо указать имя.")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Необходимо указать фамилию.")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Необходимо указать логин.")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Необходимо указать пароль.")]
        public string Password { get; set; }

        [DisplayName("День рождения")]
        [Required(ErrorMessage = "Необходимо указать день рождения пользователя.")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        [NotMap, NotEdit]
        public int Age { get { return DateTime.Now.Year - Birthday.Year; } }

        [NotDisplay, DisplayName("Роль")]
        [Required(ErrorMessage = "Необходимо выбрать роль пользователя.")]
        public virtual Role Role { get; set; }
    }
}