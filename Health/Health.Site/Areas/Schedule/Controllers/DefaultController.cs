using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Data.Repository.Fake;
using Health.Site.Areas.Schedule.Models;
using Health.Site.Attributes;
using Health.Site.Controllers;

namespace Health.Site.Areas.Schedule.Controllers
{
    public class DefaultController : CoreController
    {
        public DefaultController(IDIKernel di_kernel) : base(di_kernel)
        {
        }

        public ActionResult Index()
        {
            var view_model = new DefaultScheduleList
                                 {
                                     DefaultSchedules = CoreKernel.DefaultScheduleRepo.GetAll()
                                 };
            return View(view_model);
        }

        [PRGImport(ParametersHook = true)]
        public ActionResult Edit(int schedule_id = 1)
        {
            DefaultSchedule default_schedule = CoreKernel.DefaultScheduleRepo.GetById(schedule_id);
            var view_model = new DefaultScheduleFormModel
                                 {
                                         DefaultSchedule = default_schedule
                                 };
            return View(view_model);
        }

        [PRGExport(ParametersHook = true)]
        public ActionResult EditSubmit(DefaultScheduleFormModel form_model)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DefaultScheduleRepo.Update(form_model.DefaultSchedule);
                return RedirectTo<DefaultController>(a => a.Index());
            }
            return RedirectTo<DefaultController>(a => a.Edit(form_model.DefaultSchedule.Id));
        }
        
        [PRGImport(ParametersHook = true)]
        public ActionResult Add(DefaultScheduleFormModel form_model)
        {
            form_model = form_model ?? new DefaultScheduleFormModel
                                           {
                                               DefaultSchedule = new DefaultSchedule()
                                           };
            return View(form_model);
        }

        [PRGExport(ParametersHook = true)]
        public ActionResult AddSubmit([Bind(Include = "DefaultSchedule")]DefaultScheduleFormModel form_model)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DefaultScheduleRepo.Save(form_model.DefaultSchedule);
                return RedirectTo<DefaultController>(a => a.Index());
            }
            return RedirectTo<DefaultController>(a => a.Add(form_model));
        }

        public ActionResult Delete(int schedule_id)
        {
            CoreKernel.DefaultScheduleRepo.DeleteById(schedule_id);
            return RedirectTo<DefaultController>(a => a.Index());
        }
    }
}
