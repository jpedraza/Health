using System;
using System.Linq;
using System.Collections.Generic;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class AppointmentFakeRepository : CoreFakeRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentFakeRepository(IDIKernel diKernel) : base(diKernel)
        {
            Save(new Appointment
                     {
                         Doctor = Get<IDoctorRepository>().GetById(1),
                         Patient = Get<IPatientRepository>().GetById(1),
                         Date = DateTime.Now
                     });
            Save(new Appointment
                     {
                         Doctor = Get<IDoctorRepository>().GetById(1),
                         Patient = Get<IPatientRepository>().GetById(2),
                         Date = DateTime.Now.AddDays(-2)
                     });
            Save(new Appointment
                     {
                         Doctor = Get<IDoctorRepository>().GetById(2),
                         Patient = Get<IPatientRepository>().GetById(2),
                         Date = DateTime.Now.AddDays(-3)
                     });
            Save(new Appointment
                     {
                         Doctor = Get<IDoctorRepository>().GetById(2),
                         Patient = Get<IPatientRepository>().GetById(1),
                         Date = DateTime.Now.AddDays(3)
                     });
            Save(new Appointment
                     {
                         Doctor = Get<IDoctorRepository>().GetById(1),
                         Patient = Get<IPatientRepository>().GetById(1),
                         Date = new DateTime(2011, 10, 4, 12, 0, 0)
                     });
        }

        #region Implementation of IAppointmentRepository

        /// <summary>
        /// Получить все записи на прием для доктора на сегодня.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <returns>Записи на прием.</returns>
        public IEnumerable<Appointment> GetNowDayAppointmentsForDoctor(int doctorId)
        {
            return
                _entities.Where(
                    e => e.Doctor.Id == doctorId && e.Date.ToShortDateString() == DateTime.Now.ToShortDateString());
        }

        /// <summary>
        /// Получить все записи на прием для пациента на сегодня.
        /// </summary>
        /// <param name="patientId">Идентификатор пациента.</param>
        /// <returns>Записи на прием.</returns>
        public IEnumerable<Appointment> GetNowDayAppointmentsForPatient(int patientId)
        {
            return
                _entities.Where(
                    e => e.Patient.Id == patientId && e.Date.ToShortDateString() == DateTime.Now.ToShortDateString());
        }

        public IEnumerable<Appointment> GetAppointmentForDoctorByDate(int doctorId, DateTime date)
        {
            return
                _entities.Where(
                    e => e.Doctor.Id == doctorId && e.Date.ToShortDateString() == date.ToShortDateString());
        }

        public int CountAppointment(int doctorId, DateTime date)
        {
            return
                _entities.Where(a => a.Doctor.Id == doctorId & a.Date.ToShortDateString() == date.ToShortDateString()).
                    Count();
        }

        #endregion
    }
}
