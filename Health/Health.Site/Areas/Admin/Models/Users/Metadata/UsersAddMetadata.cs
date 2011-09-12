using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;

namespace Health.Site.Areas.Admin.Models.Users.Metadata
{
    public class UsersAddMetadata
    {
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Нужно указать имя.")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Нужно указать фамилию.")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string ThirdName { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Нужно указать логин.")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Нужно указать пароль.")]
        public string Password { get; set; }

        [DisplayName("Роль")]
        [Required(ErrorMessage = "Выберите роль пользователя.")]
        public Role Role { get; set; }

        [DisplayName("День рождения")]
        [DataType(DataType.Date, ErrorMessage = "Формат записи: 'дд/мм/гг'")]
        [Required(ErrorMessage = "Укажите день рождения пользователя.")]
        public DateTime Birthday { get; set; }
    }
}