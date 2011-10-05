using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Admin.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Admin.Controllers
{
    public class DoctorController : CoreController
    {
        public DoctorController(IDIKernel diKernel)
            : base(diKernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<DoctorController>(a => a.List());
            Doctor doctor = Get<IDoctorRepository>().GetById(id.Value);
            if (doctor == null) return RedirectTo<DoctorController>(a => a.List());
            var form = new DoctorForm { Doctor = doctor };
            return View(form);
        }

        public ActionResult List()
        {
            var form = new DoctorList { Doctors = Get<IDoctorRepository>().GetAll() };
            return View(form);
        }

        #endregion

        #region Add

        [PRGImport, ValidationModel]
        public ActionResult Add(DoctorForm form)
        {
            form.Specialties = DIKernel.Get<ISpecialtyRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel, ActionName("Add")]
        public ActionResult AddSubmit(DoctorForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IDoctorRepository>().Save(form.Doctor);
                form.Message = "Доктор добавлен";
                return RedirectTo<DoctorController>(a => a.Confirm(form));
            }
            return RedirectTo<DoctorController>(a => a.Add(form));
        }

        #endregion

        #region Edit

        [PRGImport, ValidationModel]
        public ActionResult Edit(int? id, DoctorForm form)
        {
            if (!id.HasValue) return RedirectTo<DoctorController>(a => a.List());
            form.Doctor = form.Doctor ?? Get<IDoctorRepository>().GetById(id.Value);
            if (form.Doctor == null) return RedirectTo<DoctorController>(a => a.List());
            form.Specialties = DIKernel.Get<ISpecialtyRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult Edit(DoctorForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IDoctorRepository>().Update(form.Doctor);
                form.Message = "Информация о докторе изменена";
                return RedirectTo<DoctorController>(a => a.Confirm(form));
            }
            return RedirectTo<DoctorController>(a => a.Edit(form.Doctor.Id, form));
        }

        #endregion

        #region Delete

        public ActionResult Delete(int? id, bool? confirm)
        {
            if (!id.HasValue) return RedirectTo<DoctorController>(a => a.List());
            if (!confirm.HasValue)
            {
                Doctor doctor = Get<IDoctorRepository>().GetById(id.Value);
                if (doctor == null) return RedirectTo<DoctorController>(a => a.List());
                var form = new DoctorForm { Doctor = doctor };
                return View(form);
            }
            if (confirm.Value) DIKernel.Get<IDoctorRepository>().DeleteById(id.Value);
            return RedirectTo<DoctorController>(a => a.List());
        }

        #endregion

        #region Others

        [PRGImport]
        public ActionResult Confirm(DoctorForm form)
        {
            return
                form.Doctor == null
                    ? RedirectTo<DoctorController>(a => a.List())
                    : View(form);
        }

        #endregion

        #region Schedule

        [PRGImport, ActionName("Schedule/Show")]
        public ActionResult ShowSchedule([PRGInRoute]int id)
        {
            var form = new ScheduleForm { WorkWeek = Get<IWorkWeekRepository>().GetByDoctorId(id) };
            return View("ShowSchedule", form);
        }

        [PRGImport, ValidationModel, ActionName("Schedule/Edit")]
        public ActionResult EditSchedule([PRGInRoute]int id, ScheduleForm form)
        {
            form.WorkWeek = form.WorkWeek ?? Get<IWorkWeekRepository>().GetByDoctorId(id);
            return View("EditSchedule", form);
        }

        [HttpPost, PRGExport, ValidationModel, ActionName("Schedule/Edit")]
        public ActionResult EditSchedule(ScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                form.WorkWeek.Doctor = Get<IDoctorRepository>().GetById(form.WorkWeek.Doctor.Id);
                Get<IWorkWeekRepository>().Update(form.WorkWeek);
                return RedirectTo<DoctorController>(a => a.ShowSchedule(form.WorkWeek.Doctor.Id));
            }
            return RedirectTo<DoctorController>(a => a.EditSchedule(form.WorkWeek.Doctor.Id, form));
        }
        
        #endregion

        #region Appointment

        [ActionName("Appointment/Show")]
        public ActionResult ShowAppointment(int id, int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            IEnumerable<Appointment> appointments = Get<IAttendingDoctorService>().GetAppointmentForDoctorByDate(id, date);
            var form = new AppointmentList {Appointments = appointments};
            ViewBag.Date = date;
            ViewBag.DoctorId = id;
            return View("ShowAppointment", form);
        }
        
        #endregion
    }
}
