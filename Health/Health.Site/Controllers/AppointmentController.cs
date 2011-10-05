using System;
using System.Linq;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using Health.Site.Attributes;
using Health.Site.Models;

namespace Health.Site.Controllers
{
    public class AppointmentController : CoreController
    {
        public AppointmentController(IDIKernel diKernel) : base(diKernel)
        {
        }

        [PRGImport, ValidationModel]
        public ActionResult Index(AppointmentForm form)
        {
            form.Appointment = form.Appointment ?? new Appointment{Patient = new Patient()};
            return View(form);
        }

        [HttpPost, PRGExport, ActionName("Index"), ValidationModel]
        public ActionResult IndexSubmit(AppointmentForm form)
        {
            if (ModelState.IsValid)
            {
                Patient formPatient = form.Appointment.Patient;
                if (Get<IAuthorizationService>().QuickLogin(formPatient.FirstName, formPatient.LastName,
                                                            formPatient.Birthday, formPatient.Policy))
                {
                    return RedirectTo<AppointmentController>(a => a.Doctors(form));
                }
            }
            return RedirectTo<AppointmentController>(a => a.Index(form));
        }

        [PRGImport]
        public ActionResult Doctors(AppointmentForm form)
        {
            ViewBag.Doctors = Get<IDoctorRepository>().GetAll();
            return View(form);
        }

        [ActionName("Doctor/Schedule")]
        public ActionResult DoctorSchedule(int doctorId, int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            ViewBag.Appointments = Get<IAttendingDoctorService>().GetDoctorSchedule(doctorId, date);
            ViewBag.Date = date;
            ViewBag.DoctorId = doctorId;
            return View("DoctorSchedule");
        }

        /*[HttpPost]
        public ActionResult Registration(int doctorId, DateTime date)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                                      {
                                          Date = date,
                                          Doctor = Get<IDoctorRepository>().GetById(doctorId),
                                          Patient =
                                              Get<IPatientRepository>().Find(
                                                  p =>
                                                  p.Login ==
                                                  Get<IAuthorizationService>().UserCredential.Login).
                                              FirstOrDefault()
                                      };
                Get<IAppointmentRepository>().Save(appointment);
            }
            return RedirectTo<AppointmentController>(a => a.Confirm(new AppointmentForm { Appointment = appointment }));
        }*/

        public ActionResult Confirm(AppointmentForm form)
        {
            return View(form);
        }
    }
}
