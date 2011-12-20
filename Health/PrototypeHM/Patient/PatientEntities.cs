using System;
using System.ComponentModel;
using PrototypeHM.DB;
using PrototypeHM.DB.Attributes;

namespace PrototypeHM.Patient
{
    public class PatientFullData : QueryStatus
    {
        [Hide, NotEdit]
        public int Id { get; set; }
        [DisplayName(@"Карта")]
        public string Card { get; set; }
        [DisplayName(@"Полис")]
        public string Policy { get; set; }
        [DisplayName(@"Мама")]
        public string Mother { get; set; }
        [DisplayName(@"Телефон1")]
        public string Phone1 { get; set; }
        [DisplayName(@"Телефон2")]
        public string Phone2 { get; set; }
        [DisplayName(@"Дата начала обследования")]
        public DateTime StartDateOfObservation { get; set; }
        [DisplayName(@"Код функционального класса")]
        public string FunctionalClassCode { get; set; }
        [DisplayName(@"Описание функционального класса"), EditMode(Mode = EditModeEnum.Multiline)]
        public string FunctionalClassDescription { get; set; }
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
        [Hide, DisplayName(@"Токен")]
        public string Token { get; set; }
        [DisplayName(@"Роль")]
        public string Role { get; set; }
    }
}
