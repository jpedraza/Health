using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [ScaffoldTable(true), DisplayName("Пациент")]
    public class Patient : User
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
            FunctionalAbnormalities = new List<FunctionalAbnormality>();
            Parameters = new List<Parameter>();
            Diagnosis = new List<Diagnosis>();
            SurveysStorage = new List<SurveyStorage>();
        }

        [DisplayName("Полюс")]
        [Required(ErrorMessage = "Необходимо указать номер полюса.")]
        public string Policy { get; set; }

        [DisplayName("Номер карты")]
        [Required(ErrorMessage = "Необходимо указать номер карты.")]
        public string Card { get; set; }

        [DisplayName("Мама"), NotDisplay]
        [Required(ErrorMessage = "Необходимо указать ФИО мамы.")]
        public string Mother { get; set; }

        [DisplayName("Дата начала обследования")]
        [Required(ErrorMessage = "Необходимо указать дату начала обследования")]
        public DateTime StartDateOfObservation { get; set; }

        [DisplayName("Домашний телефон")]
        [Required(ErrorMessage = "Необходимо указать номер домашнего телефона.")]
        public string HomePhone { get; set; }

        [NotDisplay, DisplayName("Рабочий телефон")]
        public string WorkPhone { get; set; }

        [NotDisplay, DisplayName("Доктор")]
        [Required(ErrorMessage = "Необходимо указать лечащего врача.")]
        public virtual Doctor Doctor { get; set; }

        [NotDisplay, DisplayName("Приемы у врача")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [NotDisplay, DisplayName("Функциональный класс")]
        public virtual FunctionalClass FunctionalClass { get; set; }

        [NotDisplay, DisplayName("Функциональные нарушения")]
        public virtual ICollection<FunctionalAbnormality> FunctionalAbnormalities { get; set; }

        [NotDisplay, DisplayName("Параметры")]
        public virtual ICollection<Parameter> Parameters { get; set; }

        [NotDisplay, DisplayName("Диагнозы")]
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }

        [NotDisplay, DisplayName("Хирургические операции")]
        public virtual ICollection<SurveyStorage> SurveysStorage { get; set; }

        [NotDisplay, DisplayName("Заполненные параметры")]
        public virtual ICollection<ParameterStorage> ParametersStorages { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2}:{3})", FirstName, LastName, Policy, Card);
        }
    }
}