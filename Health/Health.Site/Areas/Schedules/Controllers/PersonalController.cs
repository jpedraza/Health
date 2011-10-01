using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class PersonalController : CoreController
    {
        public PersonalController(IDIKernel diKernel) : base(diKernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<PersonalController>(a => a.List());
            PersonalSchedule schedule = Get<IPersonalScheduleRepository>().GetById(id.Value);
            if (schedule == null) return RedirectTo<PersonalController>(a => a.List());
            var form = new PersonalScheduleForm
                           {
                               PersonalSchedule = schedule
                           };
            return View(form);
        }

        public ActionResult List()
        {
            var form = new PersonalScheduleList
                           {
                               PersonalSchedules = Get<IPersonalScheduleRepository>().GetAll()
                           };
            return View(form);
        }

        #endregion

        #region Edit

        [PRGImport, ValidationModel]
        public ActionResult Edit([PRGInRoute] int? id, PersonalScheduleForm form)
        {
            if (!id.HasValue) return RedirectTo<PersonalController>(a => a.List());
            PersonalSchedule schedule = form.PersonalSchedule ?? Get<IPersonalScheduleRepository>().GetById(id.Value);
            if (schedule == null) return RedirectTo<PersonalController>(a => a.List());
            form.PersonalSchedule = schedule;
            form.Parameters = Get<IParameterRepository>().GetAll();
            form.Patients = Get<IPatientRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult Edit(PersonalScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IPersonalScheduleRepository>().Update(form.PersonalSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<PersonalController>(a => a.Confirm(form));
            }
            return RedirectTo<PersonalController>(a => a.Edit(form.PersonalSchedule.Id, form));
        }

        #endregion

        #region Add

        [PRGImport, ValidationModel]
        public ActionResult Add(PersonalScheduleForm form)
        {
            form.Parameters = Get<IParameterRepository>().GetAll();
            form.Patients = Get<IPatientRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult AddSubmit(PersonalScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IPersonalScheduleRepository>().Save(form.PersonalSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<PersonalController>(a => a.Confirm(form));
            }
            return RedirectTo<PersonalController>(a => a.Add(form));
        }

        #endregion

        #region Delete

        public ActionResult Delete(int? id, bool? confirm)
        {
            if (!id.HasValue) return RedirectTo<PersonalController>(a => a.List());
            if (!confirm.HasValue)
            {
                PersonalSchedule schedule = Get<IPersonalScheduleRepository>().GetById(id.Value);
                if (schedule == null) return RedirectTo<PersonalController>(a => a.List());
                var form = new PersonalScheduleForm
                               {
                                   Message = "Точно удалить это расписание",
                                   PersonalSchedule = schedule
                               };
                return View(form);
            }
            if (confirm.Value) Get<IPersonalScheduleRepository>().DeleteById(id.Value);
            return RedirectTo<PersonalController>(a => a.List());
        }

        #endregion

        #region Other 

        [PRGImport]
        public ActionResult Confirm(PersonalScheduleForm form)
        {
            return
                form.PersonalSchedule == null
                    ? RedirectTo<PersonalController>(a => a.List())
                    : View(form);
        }

        #endregion
    }
}