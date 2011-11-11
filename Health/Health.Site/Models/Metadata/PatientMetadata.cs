using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;
using System.Collections;
using System.Collections.Generic;

namespace Health.Site.Models.Metadata
{
    public class PatientMetadata
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

        [DisplayName("Лечащий врач")]
        public virtual Doctor Doctor { get; set; }

        [DisplayName("ФИО матери пациента")]
        public virtual string Mother { get; set; }

        [DisplayName("ФИО отца пациента")]
        public virtual string Father { get; set; }

        [DisplayName("Основной диагноз пациента")]
        public virtual Diagnosis MainDiagnosis { get; set; }

        [DisplayName("Вторичные диагнозы")]
        public virtual IList<Diagnosis> SecondaryDiagnosises { get; set; }
    }

    public class PatientFormMetadata : PatientMetadata
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

        [ClassMetadata(typeof(DoctorIdOnlyRequired))]
        public override Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Укажите ФИО матери пациента")]
        public override string Mother { get; set; }

        [Required(ErrorMessage = "Укажите ФИО отца пациента")]
        public override string Father { get; set; }

        [ClassMetadata(typeof(DiagnosisFormMetadata))]
        public override Diagnosis MainDiagnosis { get; set; }
    }

    public class LedPatientMetadata : PatientMetadata
    {
        [Required(ErrorMessage = "Укажите пациента")]
        public override int Id { get; set; }

        [ClassMetadata(typeof(DoctorIdOnlyRequired))]
        public override Doctor Doctor { get; set; }

      
      
    }
}