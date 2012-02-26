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
            Surveys = new List<Survey>();
        }

        [Required, DisplayName("Полюс")]
        public string Policy { get; set; }

        [Required, DisplayName("Номер карты")]
        public string Card { get; set; }

        [Required, DisplayName("Мама"), NotDisplay]
        public string Mother { get; set; }

        [Required, DisplayName("Дата начала обследования")]
        public DateTime StartDateOfObservation { get; set; }

        [Required, DisplayName("Домашний телефон")]
        public string Phone1 { get; set; }

        [NotDisplay, DisplayName("Рабочий телефон")]
        public string Phone2 { get; set; }

        [Required, NotDisplay, DisplayName("Доктор")]
        public Doctor Doctor { get; set; }

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
        public virtual ICollection<Survey> Surveys { get; set; }

        [NotDisplay, DisplayName("Заполненные параметры")]
        public virtual ICollection<ParameterStorage> ParametersStorages { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2}:{3})", FirstName, LastName, Policy, Card);
        }
    }
}