using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;

namespace Health.Core.Services
{
    public class AttendingDoctorService : CoreService, IAttendingDoctorService
    {
        public AttendingDoctorService(IDIKernel diKernel)
            : base(diKernel)
        {
        }

        /// <summary>
        /// Изменить лечащего доктора у пациента.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="patientId">Идентификатор пациента.</param>
        public void SetLedDoctorForPatient(int doctorId, int patientId)
        {
            Doctor doctor = Get<IDoctorRepository>().GetByIdIfNotLedPatient(doctorId, patientId);
            Patient patient = Get<IPatientRepository>().GetByIdIfNotLedDoctor(patientId, doctorId);
            if (doctor != null && patient != null)
            {
                doctor.Patients.Add(patient);
                patient.Doctor.Patients.Remove(patient);
                patient.Doctor = doctor;
                Get<IDoctorRepository>().Update(doctor);
                Get<IDoctorRepository>().Update(patient.Doctor);
                Get<IPatientRepository>().Update(patient);
            }
        }

        /// <summary>
        /// Получить все записи на прием для доктора на сегодня.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <returns>Записи на прием.</returns>
        public IEnumerable<Appointment> GetNowDayAppointmentsForDoctor(int doctorId)
        {
            return Get<IAppointmentRepository>().GetNowDayAppointmentsForDoctor(doctorId);
        }

        /// <summary>
        /// Получить все записи на прием для пациента на сегодня.
        /// </summary>
        /// <param name="patientId">Идентификатор пациента.</param>
        /// <returns>Записи на прием.</returns>
        public IEnumerable<Appointment> GetNowDayAppointmentsForPatient(int patientId)
        {
            return Get<IAppointmentRepository>().GetNowDayAppointmentsForPatient(patientId);
        }

        public IEnumerable<Appointment> GetAppointmentForDoctorByDate(int doctorId, DateTime date)
        {
            return Get<IAppointmentRepository>().GetAppointmentForDoctorByDate(doctorId, date);
        }

        /// <summary>
        /// Получить расписание для доктора 
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="date">Дата.</param>
        /// <returns>Расписание доктора.</returns>
        public IEnumerable<Appointment> GetDoctorSchedule(int doctorId, DateTime date)
        {
            WorkWeek workWeek = Get<IWorkWeekRepository>().GetByDoctorId(doctorId);
            if (workWeek != null && workWeek.WorkDays != null && workWeek.WorkDays.Count != 0)
            {
                WorkDay workDay =
                    workWeek.WorkDays.Where(d => d.IsWeekEndDay == false && d.Day.InWeek == (int) date.DayOfWeek).
                        FirstOrDefault();
                if (workDay != null)
                {
                    IEnumerable<Appointment> appointments = GetAppointmentForDoctorByDate(doctorId, date).ToList();
                    Doctor doctor = Get<IDoctorRepository>().GetById(doctorId);
                    IList<Appointment> appointmentsSchedule = new BindingList<Appointment>();
                    bool issetFree = false;
                    for (TimeSpan i = workDay.AttendingHoursStart;
                         i < workDay.AttendingHoursEnd;
                         i = i.Add(new TimeSpan(0, 15, 0)))
                    {
                        Appointment currentAppointment = appointments.Where(a => a.Date.TimeOfDay == i).FirstOrDefault();
                        appointmentsSchedule.Add(new Appointment
                                                     {
                                                         Date = currentAppointment == null
                                                                    ? new DateTime(date.Year, date.Month, date.Day,
                                                                                   i.Hours, i.Minutes, i.Seconds)
                                                                    : currentAppointment.Date,
                                                         Doctor = doctor,
                                                         Id = currentAppointment == null
                                                                  ? default(int)
                                                                  : currentAppointment.Id
                                                     });
                        if (currentAppointment == null) issetFree = true;
                    }
                    if (issetFree) return  appointmentsSchedule;
                }
            }
            return new BindingList<Appointment>();
        }

        /// <summary>
        /// Существуют ли свободные записи для доктора на заданную дату.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="date">Дата</param>
        /// <returns>Есть или нет свободные записи.</returns>
        public bool IssetFreeAppointmentForDoctor(int doctorId, DateTime date)
        {
            Doctor doctor = Get<IDoctorRepository>().GetById(doctorId);
            if (doctor == null ||
                doctor.WorkWeek == null ||
                doctor.WorkWeek.WorkDays == null ||
                doctor.WorkWeek.WorkDays.Count == 0) return false;
            WorkDay workDay =
                doctor.WorkWeek.WorkDays.Where(d => d.Day.InWeek == (int) date.DayOfWeek).FirstOrDefault();
            if (workDay == null) return false;
            int countCurrentAppointment = Get<IAppointmentRepository>().CountAppointment(doctorId, date);
            return countCurrentAppointment != workDay.CountAppointment;
        }

        /// <summary>
        /// Получит ближайшее свободную дату для записи к доктору.
        /// </summary>
        /// <param name="doctorId">Идентификатор доктора.</param>
        /// <param name="startDate">Стартовая дата для поиска.</param>
        /// <returns>Расписание для доктора.</returns>
        public DateTime GetDateOfNearAppointment(int doctorId, DateTime startDate)
        {
            DateTime dateTime = startDate;
            for (int i = 1; i < 10; i++)
            {
                if (IssetFreeAppointmentForDoctor(doctorId, dateTime.AddDays(i)))
                {
                    return dateTime;
                }
            }
            return default(DateTime);
        }
    }
}