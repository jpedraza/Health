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
        public ActionResult Edit(int parameter_id = 1)
        {
            DefaultSchedule default_schedule = CoreKernel.DefaultScheduleRepo.GetById(parameter_id);
            var view_model = new DefaultScheduleFormModel
                                 {
                                         DefaultSchedule = default_schedule
                                 };
            return View(view_model);
        }

        [PRGExport(ParametersHook = true)]
        public ActionResult EditSubmit(DefaultScheduleFormModel view_model)
        {
            if (ModelState.IsValid)
            {
                CoreKernel.DefaultScheduleRepo.Save(view_model.DefaultSchedule);
                return RedirectTo<DefaultController>(a => a.Index());
            }
            return RedirectTo<DefaultController>(a => a.Edit(view_model.DefaultSchedule.Parameter.ParameterId));
        }
    }
}
