using System;
using System.ComponentModel.DataAnnotations;
using Health.Core.Entities.POCO;
using Health.Core.TypeProvider;

namespace Health.Site.Models
{
    public class AppointmentForm : CoreViewModel
    {
        [ClassMetadata(typeof(AppointmentFormMetadata))]
        public Appointment Appointment { get; set; }

        private class AppointmentFormMetadata
        {
            [ClassMetadata(typeof(PatientAppointmentForm))]
            public Patient Patient { get; set; }

            private class PatientAppointmentForm
            {
                [Required(ErrorMessage = "Укажите свое имя")]
                public string FirstName { get; set; }

                [Required(ErrorMessage = "Укажите свою фамилию")]
                public string LastName { get; set; }

                [Required(ErrorMessage = "Укажите день своего рождения")]
                public DateTime Birthday { get; set; }

                [Required(ErrorMessage = "Укажите номер своего полюса")]
                public string Policy { get; set; }
            }

            public Doctor Doctor { get; set; }
        }
    }
}