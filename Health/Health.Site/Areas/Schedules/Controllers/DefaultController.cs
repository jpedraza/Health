using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Core.TypeProvider;
using Health.Site.Areas.Schedules.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;
using Health.Site.Models.Metadata;

namespace Health.Site.Areas.Schedules.Controllers
{
    public class DefaultController : CoreController
    {
        public DefaultController(IDIKernel diKernel) : base(diKernel)
        {
        }

        #region Show

        public ActionResult Show(int? id)
        {
            if (!id.HasValue) return RedirectTo<DefaultController>(a => a.List());
            DefaultSchedule schedule = Get<IDefaultScheduleRepository>().GetById(id.Value);
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
            var listForm = new DefaultScheduleList
                                {
                                    DefaultSchedules = Get<IDefaultScheduleRepository>().GetAll()
                                };
            return View(listForm);
        }

        #endregion

        #region Edit

        [PRGImport, ValidationModel]
        public ActionResult Edit([PRGInRoute] int? id, DefaultScheduleForm form)
        {
            Get<DynamicMetadataRepository>().Bind(typeof(Period), typeof(PeriodMetadata));
            if (!id.HasValue) return RedirectTo<DefaultController>(a => a.List());
            DefaultSchedule schedule = form.DefaultSchedule ?? Get<IDefaultScheduleRepository>().GetById(id.Value);
            form.DefaultSchedule = schedule;
            form.Parameters = Get<IParameterRepository>().GetAll();
            TypeDescriptor.AddProviderTransparent(
                new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Week), typeof(WeekMetadata)), form.DefaultSchedule.Week);
            return
                schedule == null
                    ? RedirectTo<DefaultController>(a => a.List())
                    : View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult Edit(DefaultScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IDefaultScheduleRepository>().Update(form.DefaultSchedule);
                form.Message = "Расписание отредактировано";
                return RedirectTo<DefaultController>(a => a.Confirm(form));
            }
            return RedirectTo<DefaultController>(a => a.Edit(form.DefaultSchedule.Id, form));
        }

        #endregion

        #region Add

        [PRGImport, ValidationModel]
        public ActionResult Add(DefaultScheduleForm form)
        {
            form.Parameters = Get<IParameterRepository>().GetAll();
            return View(form);
        }

        [HttpPost, PRGExport, ValidationModel]
        public ActionResult AddSubmit(DefaultScheduleForm form)
        {
            if (ModelState.IsValid)
            {
                Get<IDefaultScheduleRepository>().Save(form.DefaultSchedule);
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
                DefaultSchedule schedule = Get<IDefaultScheduleRepository>().GetById(id.Value);
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
            if (confirm.Value) Get<IDefaultScheduleRepository>().DeleteById(id.Value);
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