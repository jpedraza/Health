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

        public ActionResult Index()
        {
            var form_model = new PersonalScheduleList
                                 {
                                     PersonalSchedules = CoreKernel.PersonalScheduleRepo.GetAll()
                                 };
            return View(form_model);
        }

        #endregion

        [PRGImport]
        public ActionResult Edit(PersonalScheduleForm form)
        {
            return View();
        }

        [HttpPost, PRGExport]
        public ActionResult EditSubmit(PersonalScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                return RedirectTo<PersonalController>(a => a.Index());
            }
            return RedirectTo<PersonalController>(a => a.Index());
        }

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
                           ? RedirectTo<PersonalController>(a => a.Index())
                           : View(form);
            }
            if (confirm.Value) CoreKernel.PersonalScheduleRepo.DeleteById(schedule_id);
            return RedirectTo<PersonalController>(a => a.Index());
        }

        #endregion
    }
}