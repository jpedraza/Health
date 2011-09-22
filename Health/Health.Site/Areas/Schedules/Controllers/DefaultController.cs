using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Metadata;
using Health.Site.Models.Providers;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class DefaultController : CoreController
    {
        public DefaultController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        #region Show

        public ActionResult Show(int schedule_id)
        {
            var form = new DefaultScheduleForm
                           {
                               DefaultSchedule = CoreKernel.DefaultScheduleRepo.GetById(schedule_id)
                           };
            return View(form);
        }

        public ActionResult List()
        {
            var list_form = new DefaultScheduleList
                                {
                                    DefaultSchedules = CoreKernel.DefaultScheduleRepo.GetAll()
                                };
            return View(list_form);
        }

        #endregion

        #region Edit

        [PRGImport(ParametersHook = true)]
        public ActionResult Edit(int schedule_id = 1)
        {
            ClassMetadataBinder<DefaultSchedule, DefaultScheduleEditMetadata>();
            DefaultSchedule schedule = CoreKernel.DefaultScheduleRepo.GetById(schedule_id);
            var form = new DefaultScheduleForm
                           {
                               DefaultSchedule = schedule,
                               Parameters = CoreKernel.ParamRepo.GetAll()
                           };
            return
                schedule == null
                    ? RedirectTo<DefaultController>(a => a.List())
                    : View(form);
        }

        [HttpPost, PRGExport(ParametersHook = true)]
        public ActionResult Edit(DefaultScheduleForm form)
        {
            ClassMetadataBinder<DefaultSchedule, DefaultScheduleEditMetadata>();
            if (ModelState.IsValid)
            {
                CoreKernel.DefaultScheduleRepo.Update(form.DefaultSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<DefaultController>(a => a.Confirm(form));
            }
            return RedirectTo<DefaultController>(a => a.Edit(form.DefaultSchedule.Id));
        }

        #endregion

        #region Add

        [PRGImport(ParametersHook = true)]
        public ActionResult Add(int? schedule_id)
        {
            DefaultSchedule schedule = !schedule_id.HasValue
                                           ? new DefaultSchedule()
                                           : CoreKernel.DefaultScheduleRepo.GetById(schedule_id.Value);
            var form = new DefaultScheduleForm
                           {
                               DefaultSchedule = schedule,
                               Parameters = CoreKernel.ParamRepo.GetAll()
                           };
            return View(form);
        }

        [HttpPost, PRGExport(ParametersHook = true)]
        public ActionResult Add(DefaultScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DefaultScheduleRepo.Save(form.DefaultSchedule);
                form.Message = "Расписание добавлено";
                return RedirectTo<DefaultController>(a => a.Confirm(form));
            }
            return RedirectTo<DefaultController>(a => a.Add(form));
        }

        #endregion

        #region Delete

        public ActionResult Delete(int schedule_id, bool? confirm)
        {
            if (!confirm.HasValue)
            {
                DefaultSchedule schedule = CoreKernel.DefaultScheduleRepo.GetById(schedule_id);
                var form = new DefaultScheduleForm
                               {
                                   DefaultSchedule = schedule,
                                   Message = "Точно удалить расписание?"
                               };
                return
                    schedule == null
                        ? RedirectTo<DefaultController>(a => a.List())
                        : View(form);
            }
            if (confirm.Value) CoreKernel.DefaultScheduleRepo.DeleteById(schedule_id);
            return RedirectTo<DefaultController>(a => a.List());
        }

        #endregion

        #region Other

        [PRGImport(ParametersHook = true)]
        public ActionResult Confirm(DefaultScheduleForm form)
        {
            return
                form.DefaultSchedule == null
                    ? RedirectTo<DefaultController>(a => a.List())
                    : View(form);
        }

        #endregion
    }
}