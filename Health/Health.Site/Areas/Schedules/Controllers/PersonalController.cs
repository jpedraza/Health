using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class PersonalController : CoreController
    {
        public PersonalController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        #region Show

        public ActionResult Show(int schedule_id)
        {
            var form = new PersonalScheduleForm
                           {
                               PersonalSchedule = CoreKernel.PersonalScheduleRepo.GetById(schedule_id)
                           };
            return View(form);
        }

        public ActionResult List()
        {
            var form = new PersonalScheduleList
                           {
                               PersonalSchedules = CoreKernel.PersonalScheduleRepo.GetAll()
                           };
            return View(form);
        }

        #endregion

        #region Edit

        [PRGImport(ParametersHook = true)]
        public ActionResult Edit(int schedule_id = 1)
        {
            PersonalSchedule schedule = CoreKernel.PersonalScheduleRepo.GetById(schedule_id);
            var form = new PersonalScheduleForm
                           {
                               PersonalSchedule = schedule,
                               Parameters = CoreKernel.ParamRepo.GetAll(),
                               Patients = CoreKernel.PatientRepo.GetAll()
                           };
            return View(form);
        }

        [HttpPost, PRGExport(ParametersHook = true)]
        public ActionResult Edit(PersonalScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.PersonalScheduleRepo.Update(form.PersonalSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<PersonalController>(a => a.Confirm(form));
            }
            return RedirectTo<PersonalController>(a => a.Edit(form.PersonalSchedule.Id));
        }

        #endregion

        #region Add

        [PRGImport(ParametersHook = true)]
        public ActionResult Add(int? schedule_id)
        {
            PersonalSchedule schedule = !schedule_id.HasValue
                                            ? new PersonalSchedule()
                                            : CoreKernel.PersonalScheduleRepo.GetById(schedule_id.Value);
            var form = new PersonalScheduleForm
                           {
                               PersonalSchedule = schedule,
                               Parameters = CoreKernel.ParamRepo.GetAll(),
                               Patients = CoreKernel.PatientRepo.GetAll()
                           };
            return View(form);
        }

        [HttpPost, PRGExport(ParametersHook = true)]
        public ActionResult Add(PersonalScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.PersonalScheduleRepo.Save(form.PersonalSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<PersonalController>(a => a.Confirm(form));
            }
            return RedirectTo<PersonalController>(a => a.Add(form.PersonalSchedule.Id));
        }

        #endregion

        #region Delete

        public ActionResult Delete(int schedule_id, bool? confirm)
        {
            if (!confirm.HasValue)
            {
                PersonalSchedule schedule = CoreKernel.PersonalScheduleRepo.GetById(schedule_id);
                var form = new PersonalScheduleForm
                               {
                                   Message = "Точно удалить это расписание",
                                   PersonalSchedule = schedule
                               };
                return schedule == null
                           ? RedirectTo<PersonalController>(a => a.List())
                           : View(form);
            }
            if (confirm.Value) CoreKernel.PersonalScheduleRepo.DeleteById(schedule_id);
            return RedirectTo<PersonalController>(a => a.List());
        }

        #endregion

        #region Other 

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