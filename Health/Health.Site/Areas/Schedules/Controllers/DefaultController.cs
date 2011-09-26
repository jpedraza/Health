using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class DefaultController : CoreController
    {
        public DefaultController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<DefaultController>(a => a.List());
            DefaultSchedule schedule = CoreKernel.DefaultScheduleRepo.GetById(id.Value);
            var form = new DefaultScheduleForm
                           {
                               DefaultSchedule = schedule
                           };
            return
                schedule == null
                    ? RedirectTo<DefaultController>(a => a.List())
                    : View(form);
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

        [PRGImport]
        public ActionResult Edit([PRGInRoute] int? id, DefaultScheduleForm form)
        {
            if (!id.HasValue) return RedirectTo<DefaultController>(a => a.List());
            DefaultSchedule schedule = form.DefaultSchedule ?? CoreKernel.DefaultScheduleRepo.GetById(id.Value);
            form.DefaultSchedule = schedule;
            form.Parameters = CoreKernel.ParamRepo.GetAll();
            return
                schedule == null
                    ? RedirectTo<DefaultController>(a => a.List())
                    : View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult Edit(DefaultScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DefaultScheduleRepo.Update(form.DefaultSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<DefaultController>(a => a.Confirm(form));
            }
            return RedirectTo<DefaultController>(a => a.Edit(form.DefaultSchedule.Id, form));
        }

        #endregion

        #region Add

        [PRGImport]
        public ActionResult Add(DefaultScheduleForm form)
        {
            form.Parameters = CoreKernel.ParamRepo.GetAll();
            return View(form);
        }

        [HttpPost, PRGExport]
        public ActionResult AddSubmit(DefaultScheduleForm form)
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

        public ActionResult Delete(int? id, bool? confirm)
        {
            if (!id.HasValue) return RedirectTo<DefaultController>(a => a.List());
            if (!confirm.HasValue)
            {
                DefaultSchedule schedule = CoreKernel.DefaultScheduleRepo.GetById(id.Value);
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
            if (confirm.Value) CoreKernel.DefaultScheduleRepo.DeleteById(id.Value);
            return RedirectTo<DefaultController>(a => a.List());
        }

        #endregion

        #region Other

        [PRGImport]
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