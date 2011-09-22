using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Attributes;

namespace Health.Site.Models.Metadata
{
    public class PersonalScheduleMetadata
    {
        [DisplayName("Идентификатор расписания")]
        public int Id { get; set; }

        [DisplayName("Пациент")]
        [ClassMetadata(typeof(IfSubPatientMetadata))]
        public Patient Patient { get; set; }

        [DisplayName("Параметр")]
        [ClassMetadata(typeof(IfSubParameterMetadata))]
        public Parameter Parameter { get; set; }

        [DisplayName("Дата начала")]
        [Required(ErrorMessage = "Необходимо указать дату начала ввода параметра")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        [Required(ErrorMessage = "Необходимо указать дату окончаня ввода параметра")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Время начальное")]
        [Required(ErrorMessage = "Необходимо указать время ввода параметра")]
        public TimeSpan TimeStart { get; set; }

        [DisplayName("Время окончания")]
        [Required(ErrorMessage = "Необходимо указать время ввода параметра")]
        public TimeSpan TimeEnd { get; set; }

        [DisplayName("День")]
        public Day Day { get; set; }

        [DisplayName("Неделя")]
        public Week Week { get; set; }

        [DisplayName("Месяц")]
        public Month Month { get; set; }
    }

    public class PersonalScheduleEditMetadata : PersonalScheduleMetadata
    {
        [Required(ErrorMessage = "Выберите расписание")]
        public new int Id { get; set; }
    }
}