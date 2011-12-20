using System;
using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM.User
{
    public class UserFullData : QueryStatus
    {
        [DisplayName(@"Идентификатор"), Hide]
        public int Id { get; set; }

        [DisplayName(@"Имя")]
        public string FirstName { get; set; }

        [DisplayName(@"Фамилия")]
        public string LastName { get; set; }

        [DisplayName(@"Отчество")]
        public string ThirdName { get; set; }

        [DisplayName(@"Логин")]
        public string Login { get; set; }

        [DisplayName(@"Пароль")]
        public string Password { get; set; }

        [DisplayName(@"Роль")]
        public string Role { get; set; }

        [DisplayName(@"День рождения")]
        public DateTime Birthday { get; set; }

        [DisplayName(@"Токен"), Hide]
        public string Token { get; set; }
    }
}
